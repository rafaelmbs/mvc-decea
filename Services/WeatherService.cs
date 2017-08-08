using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc_decea.Repository.Repositories;
using mvc_decea.Repository.Views;
using Newtonsoft.Json;

namespace mvc_decea.Services
{
    public class WeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<IList<WeatherMetView>> GetWeather(string icao)
        {
            var result = await _weatherRepository.GetWeather(icao);

            return result;
        }
    }
}