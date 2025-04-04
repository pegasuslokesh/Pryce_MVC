using Microsoft.AspNetCore.Mvc;
using Pryce_MVC.Models;
using Pryce_MVC.Repositories;
using Pryce_MVC.Services;
using System.Linq.Dynamic.Core;
using X.PagedList.Extensions;

namespace Pryce_MVC.Controllers
{
    public class IT_ModuleMasterController : Controller
    {

        //private readonly ILogger<IT_ModuleMasterController> _logger;
        private readonly IMenuRepository _menuRepository;
        private readonly ISPRepository _ISPRepository;
        private readonly ISPService _spService;

        public IT_ModuleMasterController(ISPRepository ispRepository, ISPService spService)

        {
            _ISPRepository = ispRepository;
            _spService = spService;
            //_logger = logger;
        }


        public async Task<IActionResult> Index(int optype = 1)
        {
            var masterModules = await _ISPRepository.sp_IT_ModuleMaster_SelectRow(0, null, optype);
            var objectModules = await _ISPRepository.sp_IT_ObjectEntry_SelectRow(0, null, optype);
            var moduleObjects = await _ISPRepository.sp_IT_App_Mod_Object_SelectRow(0, 1); // Fetch module-object relationships

            //if (masterModules == null || !masterModules.Any())
            //    _logger.LogWarning("No modules found from stored procedure.");

            //if (objectModules == null || !objectModules.Any())
            //    _logger.LogWarning("No objects found from stored procedure.");



            ViewBag.MasterModules = masterModules; // Store Modules
            ViewBag.ObjectEntries = objectModules; // Store Objects
            ViewBag.ModuleObjects = moduleObjects; // Store module-object relationships

            return View();
        }



        //adding new line BY mohammed 3:44PM 04/02/2025

        public async Task<IActionResult> ListPartial(int page = 1, int pageSize = 10, string sortColumn = "Module_Id", string sortOrder = "asc", string searchColumn = "", string searchType = "", string searchText = "", int optype = 1)
        {
            // Fetch data using stored procedure service
            var masterModules = await _spService.sp_IT_ModuleMaster_SelectRowNew(0, null, optype);
            if (masterModules != null)
            {
                string print = "hi";
            }
            // Apply Searching
            if (!string.IsNullOrEmpty(searchColumn) && !string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();

                masterModules = masterModules.Where(r =>
                        (searchColumn == "Module_Id" && r.Module_Id.ToString().Contains(searchText)) ||
                        (searchColumn == "Module_Name" && MatchSearch(r.Module_Name, searchText, searchType)) ||
                        (searchColumn == "Module_Name_L" && MatchSearch(r.Module_Name_L, searchText, searchType))
                    //(searchColumn == "ParentCompanyName" && MatchSearch(r.ParentCompany, searchText, searchType)) ||
                    //(searchColumn == "Company_Code" && MatchSearch(r.Company_Code, searchText, searchType))
                    ).ToList();
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortColumn) && typeof(Company_Master).GetProperty(sortColumn) != null)
            {
                masterModules = sortOrder == "asc"
                        ? masterModules.OrderBy(r => typeof(IT_ModuleMaster).GetProperty(sortColumn).GetValue(r)).ToList()
                        : masterModules.OrderByDescending(r => typeof(IT_ModuleMaster).GetProperty(sortColumn).GetValue(r)).ToList();
            }

            // Apply Pagination
            var pagedList = masterModules.ToPagedList(page, pageSize);
            return PartialView("_ListPartial", pagedList);
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




        //Adding new functions in the controller by Mohammed 11:30 04/03/2025


        public async Task<IActionResult> EditPartial(int id)
        {
            int optype = 1;
            var Module_Masters = await _spService.sp_IT_ModuleMaster_SelectRowNew(id, null, optype);

            // Ensure you return a single Compny object, not a list
            var Module_Mastersnew = Module_Masters.FirstOrDefault();

            if (Module_Mastersnew == null)
            {
                return NotFound(); // Handle case where no religion is found
            }

            return PartialView("_NewPartial", Module_Mastersnew);
        }


        // Handle the case where the religion is not found (return NotFound, for example)
        public async Task<IActionResult> InactivePartial(int page = 1, int pageSize = 10, string sortColumn = "Module_Name", string sortOrder = "asc", string searchColumn = "", string searchType = "", string searchText = "", int optype = 3)
        {
            // Fetch data using stored procedure service
            var ModuleMaster = await _spService.sp_IT_ModuleMaster_SelectRowNew(0, null, optype);

            // Apply Searching
            if (!string.IsNullOrEmpty(searchColumn) && !string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();

                ModuleMaster = ModuleMaster.Where(r =>
                    (searchColumn == "Module_Id" && r.Module_Id.ToString().Contains(searchText)) ||
                    (searchColumn == "Module_Name" && MatchSearch(r.Module_Name, searchText, searchType)) ||
                    (searchColumn == "Module_Name_L" && MatchSearch(r.Module_Name_L, searchText, searchType))
                //(searchColumn == "CreatedBy" && MatchSearch(r.CreatedBy, searchText, searchType)) ||
                //(searchColumn == "ModifiedBy" && MatchSearch(r.ModifiedBy, searchText, searchType))
                ).ToList();
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortColumn) && typeof(IT_Module_MasterPage).GetProperty(sortColumn) != null)
            {
                ModuleMaster = sortOrder == "asc"
                    ? ModuleMaster.OrderBy(r => typeof(IT_Module_MasterPage).GetProperty(sortColumn).GetValue(r)).ToList()
                    : ModuleMaster.OrderByDescending(r => typeof(IT_Module_MasterPage).GetProperty(sortColumn).GetValue(r)).ToList();
            }

            // Apply Pagination
            var pagedList = ModuleMaster.ToPagedList(page, pageSize);
            return PartialView("_ListPartial", pagedList);
        }




        [HttpPost]
        public async Task<IActionResult> EditPartial(IT_Module_MasterPage model)
        {
            if (model.Module_Id == null || model.Module_Id == 0)
            {
                return PartialView("_ReligionFormPartial", model); // Return form with errors
            }

            try
            {
                //int optype = 2; // Assuming '2' is the operation type for updates
                var success = await _spService.UpdateReligionAsync(model.Module_Id, model.Module_Name, model.Module_Name_L);

                if (success > 0)
                {
                    TempData["SuccessMessage"] = "Religion updated successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Update failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("Index"); // Redirect to the main list
        }




        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Assuming `isActive = false` means soft delete
                bool isActive = false;
                string modifiedBy = "admin"; // Get the current user or use "System"
                DateTime modifiedDate = DateTime.UtcNow;

                // Call the stored procedure through your SPService
                int result = await _spService.DeleteReligionAsync(id, isActive, modifiedBy, modifiedDate);

                if (result > 0) // Check if deletion was successful
                {
                    return Json(new { success = true, message = "Record deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "No record deleted. Check if the ID exists." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting record: " + ex.Message });
            }
        }

        //Code for get all company name and after filter it according to search term. Code by jitendra singh rao 31-03-2025

        [HttpGet]
        public async Task<IActionResult> GetCompanyNames(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return Json(new { success = false, message = "Invalid search term" });
            }
            int optype = 7;


            // Fetch all companies first
            var company_Masters = await _spService.GetCompaniesAsync(0, null, optype);

            // Filter companies where name starts with 'term'
            var filteredCompanies = company_Masters
                .Where(c => c.Company_Name.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                .Select(c => new { label = c.Company_Name, value = c.Company_Name })
                .ToList();

            return Json(filteredCompanies);
        }






        //4/04/2025

        public async Task<IActionResult> InsertReligion()
        {
            //var companyMasters = await _spService.GetCompaniesAsync(0, null, 7);

            //// Pass list using ViewBag
            //ViewBag.CompanyList = companyMasters;
            //var CourrncyName = await _spService.ExecuteCurrencySPAsync(0, null, 6);
            //ViewBag.CurrencyList = CourrncyName;
            //var AddressList = await _spService.ExecuteCurrencySPAsync(0, null, 6);
            //ViewBag.CurrencyList = CourrncyName;
            return PartialView("_NewPartial");
        }


    }
}
