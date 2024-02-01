using System.Threading.Tasks;

namespace CrontabRegistry.Domain.Repositories
{
    public interface IWeatherForecastRepository
    {
        public Task<string[]> GetSummaries();
    }
}
