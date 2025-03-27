using Pryce_MVC.Models;

namespace Pryce_MVC.Services
{
    public interface ISPService
    {
        Task<IEnumerable<ReligionMaster>> GetReligionDataAsync(int religionId, string religion, int optype);
    }

}
