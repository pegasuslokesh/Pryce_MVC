using Pryce_MVC.Models;

namespace Pryce_MVC.Repositories
{
    public interface ISPRepository
    {
        Task<IEnumerable<ReligionMaster>> ExecuteReligionSPAsync(int religionId, string religion, int optype);
    }
}
