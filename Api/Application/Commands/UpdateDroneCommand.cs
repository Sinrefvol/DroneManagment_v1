using API.Application.ApiModels;
using Domain.AggregatesModel.DroneAggregate;
using MediatR;

namespace API.Application.Commands
{
    public class UpdateDroneCommand : IRequest<ApiDrone>
    {
        public UpdateDroneCommand(Guid id, DateTime scheduledMaintainance, double distanceCovered, bool onMaintainance)
        {
            Id = id;
            ScheduledMaintainance = scheduledMaintainance;
            DistanceCovered = distanceCovered;
            OnMaintainance = onMaintainance;
        }

        public Guid Id { get; init; }
        public DateTime ScheduledMaintainance { get; init; }
        public double DistanceCovered { get; init; }
        public bool OnMaintainance { get; init; }

        public class Handler : IRequestHandler<UpdateDroneCommand, ApiDrone>
        {
            private readonly IDroneRepository _repo;

            public Handler(IDroneRepository repo)
            {
                _repo = repo;
            }

            public async Task<ApiDrone> Handle(UpdateDroneCommand request, CancellationToken cancellationToken)
            {
                var drone = await _repo.GetById(request.Id);
                if (drone == null)
                    return null;
                try
                {
                    drone.UpdateOnMaintainance(request.OnMaintainance);
                    drone.UpdateMaintainanceDate(request.ScheduledMaintainance);
                    drone.UpdateDistanceCovered(request.DistanceCovered);
                }
                catch(Exception ex)
                {
                    return null;
                }

                var updatedModel = _repo.UpdateDrone(drone);
                await _repo.Save(cancellationToken);

                return new ApiDrone(updatedModel);
            }
        }
    }
}
