using API.Application.ApiModels;
using Domain.AggregatesModel.DroneAggregate;
using MediatR;

namespace API.Application.Commands
{
    public class DeleteDroneCommand : IRequest<ApiDrone>
    {
        public DeleteDroneCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; init; }

        public class Handler : IRequestHandler<DeleteDroneCommand,ApiDrone>
        {
            private readonly IDroneRepository _repo;

            public Handler(IDroneRepository repo)
            {
                _repo = repo;
            }

            public async Task<ApiDrone> Handle(DeleteDroneCommand request, CancellationToken cancellationToken)
            {
                var droneToDelete = await _repo.GetById(request.Id);
                if (droneToDelete == null)
                    return null;

                _repo.Remove(droneToDelete);
                await _repo.Save(cancellationToken);

                return new ApiDrone(droneToDelete);
            }
        }
    }
}
