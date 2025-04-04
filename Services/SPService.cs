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
    public async Task<IEnumerable<Set_AddressCategory>> sp_Set_AddressCategory_SelectRow(int AddressCategoryID, string AddressName, int optype)
    {
        return await _spRepository.sp_Set_AddressCategory_SelectRow(AddressCategoryID, AddressName, optype);
    }
    public async Task<IEnumerable<Address_Master>> sp_Set_AddressMaster_SelectRow(int Trans_Id, int Address_Category_Id, string Address_Name, int optype)
    {
        return await _spRepository.sp_Set_AddressMaster_SelectRow(Trans_Id, Address_Category_Id, Address_Name, optype);
    }
    public async Task<IEnumerable<Country_Master>> ExecuteCountrySPAsync(int Country_Id, string Country_Name, int optype)
    {
        return await _spRepository.ExecuteCountrySPAsync(Country_Id, Country_Name, optype);
    }
    public async Task<IEnumerable<State_Master>> sp_sys_statemaster_Select(int country_id, int trans_id, bool isActive, int op_type)
    {
        return await _spRepository.sp_sys_statemaster_Select(country_id, trans_id,isActive, op_type);
    }
    public async Task<List<Brand_Master>> Sp_getBrandMaster(int Company_Id, int Brand_Id, string Brand_Name, string Company_Ids, int Optype)
    {
        return await _spRepository.Sp_getBrandMaster(Company_Id, Brand_Id, Brand_Name, Company_Ids, Optype);
    }
    public async Task<List<Employee_Master>> Set_EmployeeMaster_SelectEmployeeName(int CompanyId, string EmpName)
    {
        return await _spRepository.Set_EmployeeMaster_SelectEmployeeName(CompanyId, EmpName);
    }
    #endregion
}


