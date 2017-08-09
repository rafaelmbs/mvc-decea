using System.Collections.Generic;
using System.Threading.Tasks;
using mvc_decea.Repository.Views.Weather;

namespace mvc_decea.Repository.Repositories
{
    public interface IWeatherRepository
    {
        Task<IList<WeatherItemView>> GetWeather(string icao);
    }
}