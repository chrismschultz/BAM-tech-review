using System.Data;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Data.Repositories;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Commands
{
    public class UpdatePerson : IRequest<UpdatePersonResult>
    {
        public required string CurrentName { get; set; }
        public required string NewName { get; set; }
        public int? PersonId { get; set; }
    }

    public class UpdatePersonPreProcessor : IRequestPreProcessor<UpdatePerson>
    {
        private readonly IPersonRepository _repo;

        public UpdatePersonPreProcessor(IPersonRepository repository)
        {
            _repo = repository;
        }

        public Task Process(UpdatePerson request, CancellationToken cancellationToken)
        {
            var person = _repo.GetByCurrentName(request.CurrentName, cancellationToken);
            if (person is null)
            {
                throw new ArgumentException($"Person {request.CurrentName} does not exist");
            }

            return Task.CompletedTask;
        }
    }

    public class UpdatePersonHandler : IRequestHandler<UpdatePerson, UpdatePersonResult>
    {
        private readonly IPersonRepository _repo;

        public UpdatePersonHandler(IPersonRepository repository)
        {
            _repo = repository;
        }

        public async Task<UpdatePersonResult> Handle(UpdatePerson request, CancellationToken cancellationToken)
        {
            var person = await _repo.UpdateAsync(request.CurrentName, request.NewName, cancellationToken);
            return new UpdatePersonResult
            {
                Id = person.Id,
                Name = person.Name
            };
        }
    }

    public class UpdatePersonResult : BaseResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
