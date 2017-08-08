using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using mvc_decea.Contracts.Weather;
using mvc_decea.Repository.Views;
using Newtonsoft.Json;

namespace mvc_decea.Repository.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        public async Task<IList<WeatherMetView>> GetWeather(string icao)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://54.233.79.161:3000");
                var response = await client.GetAsync($"/met/{icao}");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var rawChart = JsonConvert.DeserializeObject<WeatherGetResponse>(stringResult);
                return rawChart.aisweb.met;
            }
        }
    }
}