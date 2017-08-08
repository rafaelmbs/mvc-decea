using System.Collections.Generic;
using System.Threading.Tasks;
using mvc_decea.Repository.Views;

namespace mvc_decea.Repository.Repositories
{
    public interface IWeatherRepository
    {
        Task<IList<WeatherMetView>> GetWeather(string icao);
    }
}