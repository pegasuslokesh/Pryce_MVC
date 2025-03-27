using Pryce_MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pryce_MVC.Repositories
{
    public interface IMenuRepository
    {
        Task<List<Pryce_Master_Module>> GetMasterModulesAsync();
    }
}
