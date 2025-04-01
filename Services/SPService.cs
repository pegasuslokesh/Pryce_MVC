using Microsoft.Data.SqlClient;
using Pryce_MVC.Models;
using Pryce_MVC.Repositories;
using Pryce_MVC.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SPService : ISPService
{
    private readonly ISPRepository _spRepository;

    public SPService(ISPRepository spRepository)
    {
        _spRepository = spRepository;
    }
    //Religion master repository code 
    //created by jitendra singh rao 29/03/2025
    #region
    
    public async Task<IEnumerable<ReligionMaster>> GetReligionDataAsync(int religionId, string religion, int optype)
    {
        return await _spRepository.ExecuteReligionSPAsync(religionId, religion, optype);
    }
    public async Task<int> InsertReligionAsync(string religion, string religionL, DateTime created_Date)
    {
        return await _spRepository.InsertReligionAsync(religion, religionL, created_Date);
    }
    public async Task<int> UpdateReligionAsync(int religionId, string religion, string religionL)
    {
        return await _spRepository.UpdateReligionAsync(religionId, religion, religionL);
    }
    public async Task<int> DeleteReligionAsync(int religionId, bool isActive, string modifiedBy, DateTime modifiedDate)
    {
        return await _spRepository.DeleteReligionAsync(religionId, isActive, modifiedBy, modifiedDate);
    }
    #endregion
    //Repositry form company master 
    //created by jitendra singh rao 29/03/2025
    #region
    public async Task<List<Company_Master>> GetCompaniesAsync(int companyId, string companyName, int optype)
    {
        return await _spRepository.GetCompaniesAsync(companyId, companyName ,optype);
    }
    public async Task<IEnumerable<Currency_Master>> ExecuteCurrencySPAsync(int currencyId, string currencyName, int optype)
    {
        return await _spRepository.ExecuteCurrencySPAsync(currencyId, currencyName, optype);
    }
    #endregion
}


