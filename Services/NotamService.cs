using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc_decea.Repository.Repositories;
using mvc_decea.Repository.Views.Notam;
using Newtonsoft.Json;

namespace mvc_decea.Services
{
    public class NotamService
    {
        private readonly INotamRepository _notamRepository;
        public NotamService(INotamRepository notamRepository)
        {
            _notamRepository = notamRepository;
        }

        public async Task<IList<NotamItemView>> GetNotam(string icao)
        {
            var result = await _notamRepository.GetNotam(icao);

            return result;
        }
    }
}