namespace API.Application.Contracts
{
    public class UpdateDroneContract
    {
        public DateTime ScheduledMaintainance { get; set; }
        public double DistanceCovered { get; set; }
        public bool OnMaintainance { get; set; }
    }
}
