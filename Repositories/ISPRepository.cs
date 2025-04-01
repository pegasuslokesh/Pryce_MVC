using Pryce_MVC.Models;

namespace Pryce_MVC.Repositories
{
    public interface ISPRepository
    {
        Task<IEnumerable<ReligionMaster>> ExecuteReligionSPAsync(int religionId, string religion, int optype);
        Task<int> InsertReligionAsync(string religion, string religionL, DateTime created_Date );
        Task<int> UpdateReligionAsync(int religionId, string religion, string religionL);
        Task<int> DeleteReligionAsync(int religionId, bool isActive, string modifiedBy, DateTime modifiedDate);

        //This is for company master
        Task<List<Company_Master>> GetCompaniesAsync(int companyId, string companyName, int optype);
        Task<IEnumerable<Currency_Master>> ExecuteCurrencySPAsync(int currencyId, string currencyName, int optype);
    }
}
