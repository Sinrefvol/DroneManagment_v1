using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Tests.DTO;

namespace Tests.Integrations
{
    public class IntegrationTests : IClassFixture<ApiWebAppFactory>
    {
        private readonly HttpClient _client;
        private readonly ApiWebAppFactory _factory;

        public IntegrationTests(ApiWebAppFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task CreateDrone_ShouldBe_Successfull()
        {
            var response = await _client.PostAsJsonAsync("/api/v1/drones",
                new
                {
                    ModelName = "Falcon",
                    SerialNumber = "12345"
                });


            response.Should().BeSuccessful();
            Drone drone = await Helpers.DeserializeModel<Drone>(response);
            drone.DroneModel.SerialNumber.Should().Be("12345");

        }

        [Fact]
        public async Task GetDroneById_ShouldBe_Successfull()
        {
            var response = await _client.PostAsJsonAsync("/api/v1/drones",
                new
                {
                    ModelName = "Falcon",
                    SerialNumber = "12345"
                });
            response.Should().BeSuccessful();

            Drone drone = await Helpers.DeserializeModel<Drone>(response);

            var getResponse = await _client.GetAsync($"/api/v1/drones/{drone.Id}");
            getResponse.Should().BeSuccessful();
            Drone getDrone = await Helpers.DeserializeModel<Drone>(getResponse);
            getDrone.Id.Should().Be(drone.Id);
        }

        [Fact]
        public async Task DeleteDrone_ShouldBe_Successful()
        {
            var response = await _client.PostAsJsonAsync("/api/v1/drones",
               new
               {
                   ModelName = "Falcon",
                   SerialNumber = "12345"
               });
            response.Should().BeSuccessful();
            Drone drone = await Helpers.DeserializeModel<Drone>(response);

            var deleteResponse = await _client.DeleteAsync($"/api/v1/drones/{drone.Id}");
            deleteResponse.Should().BeSuccessful();

            var getResponse = await _client.GetAsync($"/api/v1/drones/{drone.Id}");
            getResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateDrone_ShouldBe_Successful()
        {
            var response = await _client.PostAsJsonAsync("/api/v1/drones",
               new
               {
                   ModelName = "Falcon",
                   SerialNumber = "12345"
               });
            response.Should().BeSuccessful();
            Drone drone = await Helpers.DeserializeModel<Drone>(response);

            var putResponse = await _client.PutAsJsonAsync($"/api/v1/drones/{drone.Id}", new
            {
                ScheduledMaintainance = DateTime.UtcNow.AddDays(2),
                DistanceCovered = 1000.2,
                OnMaintainance = false
            });

            putResponse.Should().BeSuccessful();

            var getResponse = await _client.GetAsync($"/api/v1/drones/{drone.Id}");
            getResponse.Should().BeSuccessful();
            Drone getDrone = await Helpers.DeserializeModel<Drone>(getResponse);
            getDrone.DistanceCovered.Should().Be(drone.DistanceCovered + 1000.2);
        }

        [Fact]
        public async Task GetDroneById_ShouldBe_NotFound_WhenUnknownGuid()
        {
            var getResponse = await _client.GetAsync($"/api/v1/drones/{Guid.NewGuid()}");
            getResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
