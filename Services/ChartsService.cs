using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc_decea.Repository.Repositories;
using mvc_decea.Repository.Views.Charts;
using Newtonsoft.Json;

namespace mvc_decea.Services
{
    public class ChartsService
    {
        private readonly IChartsRepository _chartsRepository;
        public ChartsService(IChartsRepository chartsRepository)
        {
            _chartsRepository = chartsRepository;
        }

        public async Task<IList<ChartsView>> GetCharts(string icao)
        {
            var result = await _chartsRepository.GetCharts(icao);

            return result;
        }
    }
}