using Microsoft.Extensions.DependencyInjection;
using mvc_decea.Repository.Repositories;
using mvc_decea.Services;

namespace mvc_decea.Configuration
{
    public class AppConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //Services
            services.AddTransient<AeroService>();
            services.AddTransient<ChartsService>();
            services.AddTransient<NotamService>();
            services.AddTransient<WeatherService>();

            //Contexts
            //services.AddTransient(typeof(DotzAppContext), p => new DotzAppContext(new SqlConnection(EnvOptions<AppSettings>.Options.ConnectionString_DotzApp)));

            //Repositories
            services.AddTransient<IAeroRepository, AeroRepository>();
            services.AddTransient<IChartsRepository, ChartsRepository>();
            services.AddTransient<INotamRepository, NotamRepository>();
            services.AddTransient<IWeatherRepository, WeatherRepository>();
        }
    }
}
