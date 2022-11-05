using API.Application.ApiModels;
using Domain.AggregatesModel.DroneAggregate;
using MediatR;

namespace API.Application.Commands
{
    public class AddDroneCommand : IRequest<ApiDrone>
    {
        public AddDroneCommand(string serialNumber, string name)
        {
            SerialNumber = serialNumber;
            Name = name;
        }

        public string SerialNumber { get; init; } = null!;
        public string Name { get; init; } = null!;


        public class Handler : IRequestHandler<AddDroneCommand, ApiDrone>
        {
            private readonly IDroneRepository _repo;

            public Handler(IDroneRepository repo)
            {
                _repo = repo;
            }

            public async Task<ApiDrone> Handle(AddDroneCommand request, CancellationToken cancellationToken)
            {
                var drone = _repo.Add(Drone.Create(request.SerialNumber, request.Name));
                await _repo.UnitOfWork.SaveChangesAsync(cancellationToken);
                return new ApiDrone(drone);
            }
        }
    }
}
