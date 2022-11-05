using Domain.AggregatesModel.DroneAggregate;

namespace API.Application.ApiModels
{
    public class ApiDrone
    {
        public ApiDrone(Drone drone)
        {
            Id = drone.Id;
            DroneModel = new ApiDroneModel(drone.DroneModel.Name, drone.DroneModel.SerialNumber);
            ScheduledMaintainance = drone.ScheduledMaintainance;
            DistanceCovered = drone.DistanceCovered;
            OnMaintainance = drone.OnMaintainance;
        }

        public Guid Id { get; init; }
        public ApiDroneModel DroneModel { get; init; }
        public DateTime ScheduledMaintainance { get; init; }
        public double DistanceCovered { get; init; }
        public bool OnMaintainance { get; init; }
    }

    public class ApiDroneModel
    {
        public ApiDroneModel(string name, string serialNumber)
        {
            Name = name;
            SerialNumber = serialNumber;
        }

        public string Name { get; private set; } = null!;
        public string SerialNumber { get; private set; } = null!;
    }
}
