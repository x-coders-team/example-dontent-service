using CrontabRegistry.Domain.Models;
using CrontabRegistry.Domain.Repositories;
using CrontabRegistry.Domain.Services;

namespace CrontabRegistry.Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastService(
            IWeatherForecastRepository weatherForecastRepository
        )
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public async Task<IEnumerable<WeatherForecastModel>> GenerateWeatherForecast()
        {
            var summaries = await _weatherForecastRepository.GetSummaries();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray();
        }
    }
}
