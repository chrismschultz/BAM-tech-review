using Dapper;
using MediatR;
using StargateAPI.Business.Data;
using StargateAPI.Business.Data.Repositories;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Queries
{
    public class GetPeople : IRequest<GetPeopleResult>
    {

    }

    public class GetPeopleHandler : IRequestHandler<GetPeople, GetPeopleResult>
    {
        private readonly IPersonRepository _repo;
        public GetPeopleHandler(IPersonRepository repository)
        {
            _repo = repository;
        }

        public async Task<GetPeopleResult> Handle(GetPeople request, CancellationToken cancellationToken)
        {
            var result = new GetPeopleResult();

            var people = await _repo.GetAllAsync(cancellationToken);

            result.People = (List<PersonAstronaut>)await _repo.GetAllAsync(cancellationToken);
            return result;
        }
    }

    public class GetPeopleResult : BaseResponse
    {
        public List<PersonAstronaut> People { get; set; } = new List<PersonAstronaut> { };

    }
}
