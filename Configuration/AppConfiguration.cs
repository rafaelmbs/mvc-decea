using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mvc_decea.Repository.Repositories;
using mvc_decea.Services;
using mvc_decea.Settings;

namespace mvc_decea.Configuration
{
    public class AppConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            //AppSettings
            services.Configure<AppSettings>(configuration);

            //Services
            services.AddTransient<AeroService>();
            services.AddTransient<ChartsService>();
            services.AddTransient<NotamService>();
            services.AddTransient<WeatherService>();            

            //Repositories
            services.AddTransient<IAeroRepository, AeroRepository>();
            services.AddTransient<IChartsRepository, ChartsRepository>();
            services.AddTransient<INotamRepository, NotamRepository>();
            services.AddTransient<IWeatherRepository, WeatherRepository>();
        }
    }
}
