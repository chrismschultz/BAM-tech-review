using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Data.Repositories;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Commands
{
    public class CreatePerson : IRequest<CreatePersonResult>
    {
        public required string Name { get; set; } = string.Empty;
    }

    public class CreatePersonPreProcessor : IRequestPreProcessor<CreatePerson>
    {
        private readonly IPersonRepository _repo;

        public CreatePersonPreProcessor(IPersonRepository repository)
        {
            _repo = repository;
        }

        public async Task Process(CreatePerson request, CancellationToken cancellationToken)
        {
            var exists = await _repo.ExistsAsync(request.Name, cancellationToken);
            if (exists)
            {
                throw new BadHttpRequestException($"Person {request.Name} already exists.");
            }
        }
    }

    public class CreatePersonHandler : IRequestHandler<CreatePerson, CreatePersonResult>
    {
        private readonly IPersonRepository _repo;

        public CreatePersonHandler(IPersonRepository repository)
        {
            _repo = repository;
        }

        public async Task<CreatePersonResult> Handle(CreatePerson request, CancellationToken ct)
        {
            var personId = await _repo.CreateAsync(request.Name, ct);

            return new CreatePersonResult { Id = personId };
        }
    }

    public class CreatePersonResult : BaseResponse
    {
        public int Id { get; set; }
    }
}
