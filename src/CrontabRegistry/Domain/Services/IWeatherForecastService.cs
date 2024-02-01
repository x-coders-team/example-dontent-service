using CrontabRegistry.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrontabRegistry.Domain.Services
{
    public interface IWeatherForecastService
    {
        public Task<IEnumerable<WeatherForecastModel>> GenerateWeatherForecast();
    }
}
