using FluentAssertions;
using RestAirline.Api.Resources;
using Xunit;

namespace RestAirline.Api.Tests.Home
{
    [Collection("api tests")]
    public class HomeControllerTests : TestBase
    {
        private readonly ApiTestClient _apiTestClient;

        public HomeControllerTests()
        {
            _apiTestClient = new ApiTestClient(HttpClient);
        }
        
        [Fact]
        public async void ShouldGetHome()
        {
            //Act
            var home = await _apiTestClient.Get<RestAirlineHomeResource>("api/home");

            //Assert
            home.ResourceLinks.Should().NotBeNull();
            home.ResourceLinks.Self.Should().NotBeNull();
            home.ResourceCommands.SelectJourneysCommand.Should().NotBeNull();
        }
    }
}