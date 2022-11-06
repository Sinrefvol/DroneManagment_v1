using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DTO
{

    public class Drone
    {
        public Drone()
        {
        }

        public Guid Id { get; init; }
        public DroneModel DroneModel { get; set; }
        public DateTime ScheduledMaintainance { get; set; }
        public double DistanceCovered { get; set; }
        public bool OnMaintainance { get; set; }
    }

    public class DroneModel
    {
        public DroneModel()
        {
        }

        public string Name { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
    }
}

