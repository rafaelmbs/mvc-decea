using Microsoft.Extensions.DependencyInjection;
using mvc_decea.Repository.Repositories;
using mvc_decea.Services;

namespace mvc_decea.Configuration
{
    public class WeatherConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //Services
            services.AddTransient<WeatherService>();

            //Repositories
            services.AddTransient<IWeatherRepository, WeatherRepository>();
        }
    }
}
