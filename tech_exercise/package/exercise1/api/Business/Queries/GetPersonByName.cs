using Dapper;
using MediatR;
using StargateAPI.Business.Data;
using StargateAPI.Business.Data.Repositories;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Queries
{
    public class GetPersonByName : IRequest<GetPersonByNameResult>
    {
        public required string Name { get; set; } = string.Empty;
    }

    public class GetPersonByNameHandler : IRequestHandler<GetPersonByName, GetPersonByNameResult>
    {
        private readonly IPersonRepository _repo;

        public GetPersonByNameHandler(IPersonRepository repository)
        {
            _repo = repository;
        }

        public async Task<GetPersonByNameResult> Handle(GetPersonByName request, CancellationToken cancellationToken)
        {
            var result = new GetPersonByNameResult();
            result.Person = await _repo.GetByNameAsync(request.Name, cancellationToken);
            return result;
        }
    }

    public class GetPersonByNameResult : BaseResponse
    {
        public PersonAstronaut? Person { get; set; }
    }
}
