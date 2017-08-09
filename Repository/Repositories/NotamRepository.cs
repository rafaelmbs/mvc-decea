using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using mvc_decea.Contracts.Notam;
using mvc_decea.Repository.Views.Notam;
using Newtonsoft.Json;

namespace mvc_decea.Repository.Repositories
{
    public class NotamRepository : INotamRepository
    {
        public async Task<IList<NotamItemView>> GetNotam(string icao)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://54.233.79.161:3000");
                var response = await client.GetAsync($"/notam/{icao}");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var rawChart = JsonConvert.DeserializeObject<NotamGetResponse>(stringResult);
                return rawChart.aisweb.notam[0].item;
            }
        }
    }
}