using System.Collections.Generic;
using System.Threading.Tasks;
using mvc_decea.Repository.Views.Notam;

namespace mvc_decea.Repository.Repositories
{
    public interface INotamRepository
    {
        Task<IList<NotamItemView>> GetNotam(string icao);
    }
}