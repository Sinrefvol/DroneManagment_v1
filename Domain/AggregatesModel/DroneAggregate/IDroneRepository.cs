using Domain.SeedWork;

namespace Domain.AggregatesModel.DroneAggregate
{
    public interface IDroneRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Drone Add(Drone drone);
        Drone Remove(Drone drone);
        Task<Drone> GetById(Guid id);
        Drone UpdateDrone(Drone drone);
    } 
}
