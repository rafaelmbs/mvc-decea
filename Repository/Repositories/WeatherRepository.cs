using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using mvc_decea.Contracts.Weather;
using mvc_decea.Repository.Views.Weather;
using mvc_decea.Settings;
using Newtonsoft.Json;

namespace mvc_decea.Repository.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IOptions<AppSettings> _config;

        public WeatherRepository(IOptions<AppSettings> config)
        {
            _config = config;
        }
        public async Task<IList<WeatherItemView>> GetWeather(string icao)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.Value.ConnectionURI_ApiDecea);
                var response = await client.GetAsync($"/met/{icao}");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var rawChart = JsonConvert.DeserializeObject<WeatherGetResponse>(stringResult);
                return rawChart.aisweb.met;
            }
        }
    }
}