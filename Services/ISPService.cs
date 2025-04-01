using Pryce_MVC.Models;

namespace Pryce_MVC.Services
{
    public interface ISPService
    {
        Task<IEnumerable<ReligionMaster>> GetReligionDataAsync(int religionId, string religion, int optype);
        Task<int> InsertReligionAsync(string religion, string religion_l, DateTime created_Date);
        Task<int> UpdateReligionAsync(int religionId, string religion, string religion_l );
        Task<int> DeleteReligionAsync(int religionId, bool isActive, string modifiedBy, DateTime modifiedDate);
        Task<List<Company_Master>> GetCompaniesAsync(int companyId, string companyName, int optype);
        Task<IEnumerable<Currency_Master>> ExecuteCurrencySPAsync(int currencyId, string currencyName, int optype);
    }
}
