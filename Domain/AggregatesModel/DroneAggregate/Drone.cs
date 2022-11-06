

using System.Xml.Linq;

namespace Domain.AggregatesModel.DroneAggregate
{
    public class Drone
    {
        public Drone()
        {
        }

        public Drone(Guid id, DroneModel droneModel, DateTime scheduledMaintainance, double distanceCovered, bool onMaintainance)
        {
            Id = id;
            DroneModel = droneModel;
            ScheduledMaintainance = scheduledMaintainance;
            DistanceCovered = distanceCovered;
            OnMaintainance = onMaintainance;
        }

        public Guid Id { get; private set; }
        public DroneModel DroneModel { get; private set; }
        public DateTime ScheduledMaintainance { get; private set; }
        public double DistanceCovered { get; private set; }
        public bool OnMaintainance { get; private set; }

        public static Drone Create(string serialNumber, string name, double distanceCovered = 0.00, bool onMaintainance = false)
        {
            Drone drone = new Drone(Guid.NewGuid(), new DroneModel(name,serialNumber), DateTime.Now.AddMonths(2), distanceCovered, onMaintainance);

            return drone;
        }

        public void UpdateMaintainanceDate(DateTime maintainance)
        {
            if (maintainance < DateTime.Now)
                throw new ArgumentException("Cannot schedule maintainance in the past");
            ScheduledMaintainance = maintainance;
        }

        public void UpdateDistanceCovered(double distanceCovered)
        {
            if (distanceCovered <= 0)
                throw new ArgumentException("Distance covered cannot be 0 or less");

            DistanceCovered += distanceCovered;
        }

        public void UpdateOnMaintainance(bool onMaintainance)
        {
            OnMaintainance = onMaintainance;
        }
    }
}
