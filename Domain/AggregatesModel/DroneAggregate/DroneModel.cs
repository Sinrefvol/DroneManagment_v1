using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregatesModel.DroneAggregate
{
    public class DroneModel
    {
        public DroneModel()
        {
        }

        public DroneModel(string name, string serialNumber)
        {
            Name = name;
            SerialNumber = serialNumber;
        }

        public string Name { get; private set; } = null!;
        public string SerialNumber { get; private set; } = null!;

        public override bool Equals(object? obj)
        {
            return obj is DroneModel model &&
                   Name == model.Name &&
                   SerialNumber == model.SerialNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, SerialNumber);
        }
    }
}
