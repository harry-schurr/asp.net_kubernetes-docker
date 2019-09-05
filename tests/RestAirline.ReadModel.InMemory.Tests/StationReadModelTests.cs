using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using RestAirline.Commands.Journey;
using RestAirline.Domain.Booking;
using RestAirline.ReadModel.InMemory;
using RestAirline.Shared;
using RestAirline.Shared.ModelBuilders;
using Xunit;

namespace RestAirline.ReadModel.Tests
{
    public class StationReadModelTests : TestBase
    {
        private readonly IInMemoryReadStore<StationsReadModel> _stationsReadModel;

        public StationReadModelTests()
        {
            _stationsReadModel = Resolver.Resolve<IInMemoryReadStore<StationsReadModel>>();
        }
        
        [Fact]
        public async Task WhenSendSelectJourneysCommandTwiceShouldGetTwoJourneysInReadModel()
        {
            //Arrange
            var journeys = new JourneysBuilder().BuildJourneys();
            var selectJourneysCommand1 = new SelectJourneysCommand(BookingId.New, journeys);
            var selectJourneysCommand2 = new SelectJourneysCommand(BookingId.New, journeys);
            
            //Act
            await CommandBus.PublishAsync(selectJourneysCommand1, CancellationToken.None);
            await CommandBus.PublishAsync(selectJourneysCommand2, CancellationToken.None);
            
            //Assert
            var readModels = await _stationsReadModel.FindAsync(rm => true, CancellationToken.None);
            var stations = readModels.SelectMany(x => x.Items).Where(x => x.ArriveStation == "SYD");

            stations.Should().HaveCount(2);
        }

    }
}