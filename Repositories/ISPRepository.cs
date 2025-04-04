using Pryce_MVC.Models;

namespace Pryce_MVC.Repositories
{
    public interface ISPRepository
    {
        Task<IEnumerable<IT_ObjectEntry>> sp_IT_ObjectEntry_SelectRow(int? objectId, string? objectName, int optype);
        Task<IEnumerable<IT_ModuleMaster>> sp_IT_ModuleMaster_SelectRow(int Application_Id, string Application_Name, int optype);
        Task<IEnumerable<IT_App_Mod_Object>> sp_IT_App_Mod_Object_SelectRow(int Application_Id, int optype);
        Task<List<IT_Module_MasterPage>> sp_IT_ModuleMaster_SelectRowNew(int Module_Id, string Module_Name, int optype);
        Task<IEnumerable<ReligionMaster>> ExecuteReligionSPAsync(int religionId, string religion, int optype);
        Task<int> InsertReligionAsync(string religion, string religionL, DateTime created_Date );
        Task<int> UpdateReligionAsync(int religionId, string religion, string religionL);
        Task<int> DeleteReligionAsync(int religionId, bool isActive, string modifiedBy, DateTime modifiedDate);

        //This is for company master
        Task<List<Company_Master>> GetCompaniesAsync(int companyId, string companyName, int optype);
        Task<IEnumerable<Currency_Master>> ExecuteCurrencySPAsync(int currencyId, string currencyName, int optype);
        Task<IEnumerable<Set_AddressCategory>> sp_Set_AddressCategory_SelectRow(int AddressCategoryID, string AddressName, int optype);
        Task<IEnumerable<Address_Master>> sp_Set_AddressMaster_SelectRow(int Trans_Id, int Address_Category_Id, string Address_Name, int optype);
        Task<IEnumerable<Country_Master>> ExecuteCountrySPAsync(int Country_Id, string Country_Name, int optype);
        Task<IEnumerable<State_Master>> sp_sys_statemaster_Select(int country_id, int trans_id, bool isActive, int op_type);
        Task<List<Brand_Master>> Sp_getBrandMaster(int Company_Id, int Brand_Id, string Brand_Name, string Company_Ids, int Optype);
        Task<List<Employee_Master>> Set_EmployeeMaster_SelectEmployeeName(int CompanyId, string EmpName);
    }
}
