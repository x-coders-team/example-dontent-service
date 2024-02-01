using CrontabRegistry.Application.Services;
using CrontabRegistry.Domain.Repositories;
using CrontabRegistry.Domain.Services;
using FluentAssertions;
using Moq;

namespace Unit.CrontabRegistry.Application.Services
{
    public class WeatherForecastServiceTest
    {
        private IWeatherForecastService _sut;
        private Mock<IWeatherForecastRepository> _mockWeatherForecastRepository;

        [SetUp]
        public void SetUp()
        {
            _mockWeatherForecastRepository = new Mock<IWeatherForecastRepository>(MockBehavior.Strict);
            _sut = new WeatherForecastService(_mockWeatherForecastRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockWeatherForecastRepository.VerifyAll();
        }

        [Test]
        public async Task GenerateWeatherForecast_should_return_random_data()
        {
            // arrange
            var expectedSummaries = new[]
            {
                "Freezing",
                "Bracing",
                "Chilly",
                "Cool",
                "Mild",
                "Warm",
                "Balmy",
                "Hot",
                "Sweltering",
                "Scorching"
            };

            _mockWeatherForecastRepository.Setup(m => m.GetSummaries())
                .ReturnsAsync(expectedSummaries);

            // act
            var results = await _sut.GenerateWeatherForecast();

            // assert
            results.Should().HaveCount(5);
            results.Should().AllSatisfy(x =>
            {
                x.Summary.Should().ContainAny(expectedSummaries);
            });
        }
    }
}
