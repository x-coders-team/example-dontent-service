using CrontabRegistry.Application.Controllers;
using CrontabRegistry.Domain.Models;
using CrontabRegistry.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Unit.CrontabRegistry.Application.Controllers
{
    public class WeatherForecastControllerTest
    {
        private WeatherForecastController _sut;
        private Mock<ILogger<WeatherForecastController>> _mockLogger;
        private Mock<IWeatherForecastService> _mockWeatherForecastService;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<WeatherForecastController>>(MockBehavior.Strict);
            _mockWeatherForecastService = new Mock<IWeatherForecastService>(MockBehavior.Strict);
            _sut = new WeatherForecastController(_mockLogger.Object, _mockWeatherForecastService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockLogger.VerifyAll();
            _mockWeatherForecastService.VerifyAll();
        }

        [Test]
        public async Task WeatherForecastControllerGetWeatherForecast_should_respond_with_random_data()
        {
            // arrange
            var expectedData = new List<WeatherForecastModel>()
            {
                new WeatherForecastModel() {Date = new DateTime(), Summary = "test-string-1", TemperatureC = 1 },
                new WeatherForecastModel() {Date = new DateTime(), Summary = "test-string-2", TemperatureC = 2 },
                new WeatherForecastModel() {Date = new DateTime(), Summary = "test-string-3", TemperatureC = 3 },
                new WeatherForecastModel() {Date = new DateTime(), Summary = "test-string-4", TemperatureC = 4 },
            };

            _mockWeatherForecastService.Setup(m => m.GenerateWeatherForecast())
                .ReturnsAsync(expectedData);

            // act
            var results = await _sut.Get();

            // assert
            results.Should().NotBeNull();
            results.Should().HaveCount(expectedData.Count);
            results.Should().BeSameAs(expectedData);
        }
    }
}
