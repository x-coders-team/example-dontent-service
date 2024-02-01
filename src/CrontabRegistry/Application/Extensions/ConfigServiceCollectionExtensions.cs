using CrontabRegistry.Application.Options;
using CrontabRegistry.Application.Services;
using CrontabRegistry.Domain.Repositories;
using CrontabRegistry.Domain.Services;
using CrontabRegistry.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CrontabRegistryDatabaseOptions>(
                configuration.GetSection(nameof(CrontabRegistryDatabaseOptions))
            );

            services = AddMongoClient(services);
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>(
                serviceProvider =>
                {
                    var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
                    var databaseOptions = serviceProvider
                        .GetRequiredService<IOptions<CrontabRegistryDatabaseOptions>>().Value;

                    return new WeatherForecastRepository(mongoClient.GetDatabase(databaseOptions.DatabaseName));
                }
            );

            return services;
        }

        private static IServiceCollection AddMongoClient(IServiceCollection services)
        {
            var mongodbClient = new MongoClient();

            services.AddScoped<IMongoClient, MongoClient>(
                serviceProvider =>
                {
                    var crontabRegistryDatabaseOptions = serviceProvider
                        .GetRequiredService<IOptions<CrontabRegistryDatabaseOptions>>().Value;
                    return new MongoClient(crontabRegistryDatabaseOptions.ConnectionString);
                }
            );

            return services;
        }
    }
}
