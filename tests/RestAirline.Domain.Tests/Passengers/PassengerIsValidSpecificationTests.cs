using System.Collections.Generic;
using FluentAssertions;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Passenger;
using RestAirline.Shared;
using RestAirline.Shared.Extensions;
using RestAirline.Shared.ModelBuilders;
using RestAirline.TestsHelper;
using Xunit;

namespace RestAirline.Domain.Tests.Passengers
{
    public class PassengerIsValidSpecificationTests
    {
        [Fact]
        public void WhenPassengerNameIsEmptyShouldFailed()
        {
            // Arrange
            var passenger = new PassengerBuilder().CreatePassenger()
                .Mutate(p => p.Name = null);

            var specification = new PassengerValidationSpecification(new List<Passenger>());

            //Act
            var result = specification.IsSatisfiedBy(passenger);
            var why = specification.WhyIsNotSatisfiedBy(passenger);
            
            //Assert
            result.Should().BeFalse();
            why.Should().HaveCount(1);
        }
        
        //Assert other case of this specification
    }
}