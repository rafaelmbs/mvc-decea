using System.Collections.Generic;
using System.Threading.Tasks;
using mvc_decea.Repository.Views.Aero;

namespace mvc_decea.Repository.Repositories
{
    public interface IAeroRepository
    {
        Task<IList<AeroItemView>> GetInfo(string icao);
    }
}