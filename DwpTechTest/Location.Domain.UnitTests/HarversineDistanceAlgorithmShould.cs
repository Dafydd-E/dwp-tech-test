using FluentAssertions;
using Location.Domain.Users;
using Xunit;

namespace Location.Domain.UnitTests
{
    public class HarversineDistanceAlgorithmShould
    {
        [Fact]
        public void CalculateExpectedDistanceBetweenLondonAndCardiff()
        {
            var london = new Coordinate(51.5074, 0.1278);
            var cardiff = new Coordinate(51.4816, 3.1791);

            var algorithm = new HarversineDistanceAlgorithm();
            var result = algorithm.CalculateDistance(london, cardiff);

            result.Should().BeApproximately(211242.089, 2);
        }
    }
}
