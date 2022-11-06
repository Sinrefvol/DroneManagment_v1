namespace Domain.AggregatesModel.DroneAggregate
{
    public interface IDroneRepository
    {
        Drone Add(Drone drone);
        Drone Remove(Drone drone);
        Task<Drone> GetById(Guid id);
        Drone UpdateDrone(Drone drone);
        Task Save(CancellationToken cancellationToken);
    }
}
