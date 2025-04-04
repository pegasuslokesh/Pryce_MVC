using Microsoft.AspNetCore.Mvc;
using Pryce_MVC.Data;
using Pryce_MVC.Models;
using Pryce_MVC.Services;
using Pryce_MVC.Models;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc;
using System.Linq.Dynamic.Core;

namespace Pryce_MVC.Controllers
{
    public class Brand_MasterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISPService _spService;

        public Brand_MasterController(ISPService spService)
        {
            _spService = spService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> BrandMaster_ListPartial(int page = 1, int pageSize = 10, string sortColumn = "BrandId", string sortOrder = "asc", string searchColumn = "", string searchType = "", string searchText = "", int optype = 1)
        {
            // Fetch data using stored procedure service
            var Brand_Masters = await _spService.Sp_getBrandMaster(0,0, null,null, 7);

            if (Brand_Masters != null)
            {
                string print = "hi";
            }
            // Apply Searching
            if (!string.IsNullOrEmpty(searchColumn) && !string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();

                Brand_Masters = Brand_Masters.Where(r =>
                        (searchColumn == "Brand_Id" && r.Brand_Id.ToString().Contains(searchText)) ||
                        (searchColumn == "Company_Name" && MatchSearch(r.Brand_Name, searchText, searchType)) ||
                        (searchColumn == "Company_Name_L" && MatchSearch(r.Brand_Name_L, searchText, searchType)) ||
                        
                        (searchColumn == "Company_Code" && MatchSearch(r.Brand_Code, searchText, searchType))
                    ).ToList();
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortColumn) && typeof(Brand_Master).GetProperty(sortColumn) != null)
            {
                Brand_Masters = sortOrder == "asc"
                        ? Brand_Masters.OrderBy(r => typeof(Brand_Master).GetProperty(sortColumn).GetValue(r)).ToList()
                        : Brand_Masters.OrderByDescending(r => typeof(Brand_Master).GetProperty(sortColumn).GetValue(r)).ToList();
            }

            // Apply Pagination
            var pagedList = Brand_Masters.ToPagedList(page, pageSize);
            return PartialView("BrandMaster_ListPartial", pagedList);
        }
        private bool MatchSearch(string fieldValue, string searchText, string searchType)
        {
            if (fieldValue == null) return false;

            return searchType switch
            {
                "contains" => fieldValue.ToLower().Contains(searchText),
                "equals" => fieldValue.ToLower() == searchText,
                "startswith" => fieldValue.ToLower().StartsWith(searchText),
                "endswith" => fieldValue.ToLower().EndsWith(searchText),
                _ => false
            };
        }
        public async Task<IActionResult> InsertReligion()
        {
            var companyMasters = await _spService.GetCompaniesAsync(0, null, 7);

            // Pass list using ViewBag
            ViewBag.CompanyList = companyMasters;
            var CourrncyName = await _spService.ExecuteCurrencySPAsync(0, null, 6);
            ViewBag.CurrencyList = CourrncyName;
            var AddressList = await _spService.sp_Set_AddressCategory_SelectRow(0, null, 2);
            ViewBag.AddressList = AddressList;
            ViewBag.CurrencyList = CourrncyName;
            var CountryList = await _spService.ExecuteCountrySPAsync(0, null, 1);
            ViewBag.CountryList = CountryList;
            //var MainAddressList = await _spService.sp_Set_AddressMaster_SelectRow(0,0 ,null, 1);
            //ViewBag.MainAddressList = MainAddressList;
            return PartialView("BrandMaster_NewPartial");
        }
        [HttpGet]
        public async Task<IActionResult> GetManagerNameAll(int companyId,string term)
      {
            companyId = 2;
            if (string.IsNullOrWhiteSpace(term))
            {
                return Json(new { success = false, message = "Invalid search term" });
            }
            // int optype = 7;


            // Fetch all companies first
            var ManagerList = await _spService.Set_EmployeeMaster_SelectEmployeeName(companyId, term);

            // Filter AddressName where name starts with 'term'
            var filteredCompanies = ManagerList
                
                 .Select(c => new { label = c.Emp_Name, value = c.Emp_Name })
                 .ToList();

            return Json(filteredCompanies);
        }
        [HttpGet]
        public async Task<IActionResult> GetAddressUsingCountryId(int Countryid)
        {
            if (Countryid == 0)
            {
                return Json(new List<object>()); // Return empty if no country selected
            }

            var MainAddressList = await _spService.sp_sys_statemaster_Select(Countryid, 0, true, 1);

            var filteredStates = MainAddressList
                .Select(c => new { label = c.State_Name, value = c.State_Name })
                .ToList();

            return Json(filteredStates);
        }
        public async Task<IActionResult> UpdateBrand_Master(int id)
        {
            int optype = 1;
            var company_Masters = await _spService.GetCompaniesAsync(id, null, optype);

            // Ensure you return a single Compny object, not a list
            var company_Mastersnew = company_Masters.FirstOrDefault();

            if (company_Mastersnew == null)
            {
                return NotFound(); // Handle case where no religion is found
            }

            return PartialView("BrandMaster_NewPartial", company_Mastersnew);
        }
    }



}
