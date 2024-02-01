using CrontabRegistry.Domain.Models;
using FluentAssertions;
using Integration.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Integration.CrontabRegistry.Application.Controllers
{
    public class WeatherForecastControllerTest
    {
        private WebApplicationFactory<Program> _clientApiFactory;
        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            _clientApiFactory = new CustomWebApplicationFactory<Program>();
            _httpClient = _clientApiFactory.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _clientApiFactory.Dispose();
            _httpClient.Dispose();
        }

        [Test]
        public async Task GetWeatherForecast_should_return_random_data()
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

            // act
            var response = await _httpClient.GetAsync("WeatherForecast");
            var contentType = response.Content.Headers.ContentType!.ToString();
            var responseJsonContent = HtmlContentHelper.GetJsonContent<WeatherForecastModel[]>(
                await response.Content.ReadAsStringAsync()
                );

            // assert
            response.EnsureSuccessStatusCode();
            contentType.Should().Contain("application/json; charset=utf-8");
            responseJsonContent.Should().NotBeNullOrEmpty();
            responseJsonContent.Should().AllSatisfy(x =>
            {
                x.Summary.Should().ContainAny(expectedSummaries);
            });
        }
    }
}
