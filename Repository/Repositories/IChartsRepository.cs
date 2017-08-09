using System.Collections.Generic;
using System.Threading.Tasks;
using mvc_decea.Repository.Views.Charts;

namespace mvc_decea.Repository.Repositories
{
    public interface IChartsRepository
    {
        Task<IList<ChartsView>> GetCharts(string icao);
    }
}