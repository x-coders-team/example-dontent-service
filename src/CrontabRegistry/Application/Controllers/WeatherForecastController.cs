using CrontabRegistry.Domain.Models;
using CrontabRegistry.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrontabRegistry.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherForecastService weatherForecastService
        )
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecastModel>> Get()
        {
            return await _weatherForecastService.GenerateWeatherForecast();
        }
    }
}