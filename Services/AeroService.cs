using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc_decea.Repository.Repositories;
using mvc_decea.Repository.Views.Aero;
using Newtonsoft.Json;

namespace mvc_decea.Services
{
    public class AeroService
    {
        private readonly IAeroRepository _aeroRepository;
        public AeroService(IAeroRepository aeroRepository)
        {
            _aeroRepository = aeroRepository;
        }

        public async Task<IList<AeroItemView>> GetInfo(string icao)
        {
            var result = await _aeroRepository.GetInfo(icao);

            return result;
        }
    }
}