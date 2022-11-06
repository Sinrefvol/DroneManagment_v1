using Domain.AggregatesModel.DroneAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Domain
{
    public class DroneTests
    {
        private readonly Drone drone;

        public DroneTests()
        {
            this.drone = Drone.Create("12345", "Falcon");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void UpdateDistanceLestOrEqualToNull_ThrowsException(int distance)
        {
            Assert.Throws<ArgumentException>(() => drone.UpdateDistanceCovered(distance));
        }

        [Fact]
        public void UpdateScheduledMaintainanceLessThenToday_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => drone.UpdateMaintainanceDate(DateTime.UtcNow.AddDays(-1)));
        }

        [Fact]
        public void CreateDrone_ReturnsRightDrone()
        {
            Drone drone = Drone.Create("12345", "Falcon");

            Assert.Equal("Falcon", drone.DroneModel.Name);
            Assert.Equal("12345", drone.DroneModel.SerialNumber);
        }


    }
}
