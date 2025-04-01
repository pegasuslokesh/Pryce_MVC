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
   
        public class Company_MasterController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly ISPService _spService;

            public Company_MasterController(ISPService spService)
            {
                _spService = spService;
            }
            #region
            public async Task<IActionResult> Index()
            {
                //var religions = await _spService.GetReligionDataAsync(0, null, optype);
                return View();
            }
            // GET: Religion_Master/Details/5



            public async Task<IActionResult> ListPartial(int page = 1, int pageSize = 10, string sortColumn = "Company_Id", string sortOrder = "asc", string searchColumn = "", string searchType = "", string searchText = "", int optype = 1)
            {
                // Fetch data using stored procedure service
                var company_Masters = await _spService.GetCompaniesAsync(0, null, optype);
                if (company_Masters != null)
            {
                string print = "hi";
            }
                // Apply Searching
                if (!string.IsNullOrEmpty(searchColumn) && !string.IsNullOrEmpty(searchText))
                {
                    searchText = searchText.ToLower();

                company_Masters = company_Masters.Where(r =>
                        (searchColumn == "Company_Id" && r.Company_Id.ToString().Contains(searchText)) ||
                        (searchColumn == "Company_Name" && MatchSearch(r.Company_Name, searchText, searchType)) ||
                        (searchColumn == "Company_Name_L" && MatchSearch(r.Company_Name_L, searchText, searchType)) ||
                        (searchColumn == "ParentCompanyName" && MatchSearch(r.ParentCompany, searchText, searchType)) ||
                        (searchColumn == "Company_Code" && MatchSearch(r.Company_Code, searchText, searchType))
                    ).ToList();
                }

                // Sorting
                if (!string.IsNullOrEmpty(sortColumn) && typeof(Company_Master).GetProperty(sortColumn) != null)
                {
                company_Masters = sortOrder == "asc"
                        ? company_Masters.OrderBy(r => typeof(Company_Master).GetProperty(sortColumn).GetValue(r)).ToList()
                        : company_Masters.OrderByDescending(r => typeof(Company_Master).GetProperty(sortColumn).GetValue(r)).ToList();
                }

                // Apply Pagination
                var pagedList = company_Masters.ToPagedList(page, pageSize);
                return PartialView("_ListPartial", pagedList);
            }

            // Helper function for searching
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
            var AddressList = await _spService.ExecuteCurrencySPAsync(0, null, 6);
            ViewBag.CurrencyList = CourrncyName;
            return PartialView("_NewPartial");
        }


        [HttpPost]
            public async Task<IActionResult> InsertReligion(ReligionMaster model)
            {
                if (string.IsNullOrWhiteSpace(model.Religion) || string.IsNullOrWhiteSpace(model.Religion_L))
                {
                    return BadRequest("Religion name and local name are required.");
                }

                model.CreatedDate = DateTime.Now;
                var referenceId = await _spService.InsertReligionAsync(model.Religion, model.Religion_L, model.CreatedDate);

                if (referenceId > 0)
                {
                    TempData["SuccessMessage"] = "Religion added successfully!";
                    return RedirectToAction("Index"); // Redirect to the Index page
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to insert religion.";
                    return View(model); // Stay on the same page and show error
                }
            }



            public async Task<IActionResult> EditPartial(int id)
            {
                int optype = 1;
                var company_Masters = await _spService.GetCompaniesAsync(id, null, optype);

                // Ensure you return a single Compny object, not a list
                var company_Mastersnew = company_Masters.FirstOrDefault();

                if (company_Mastersnew == null)
                {
                    return NotFound(); // Handle case where no religion is found
                }

                return PartialView("_NewPartial", company_Mastersnew);
            }


            // Handle the case where the religion is not found (return NotFound, for example)
            public async Task<IActionResult> InactivePartial(int page = 1, int pageSize = 10, string sortColumn = "Religion", string sortOrder = "asc", string searchColumn = "", string searchType = "", string searchText = "", int optype = 3)
            {
                // Fetch data using stored procedure service
                var religions = await _spService.GetReligionDataAsync(0, null, optype);

                // Apply Searching
                if (!string.IsNullOrEmpty(searchColumn) && !string.IsNullOrEmpty(searchText))
                {
                    searchText = searchText.ToLower();

                    religions = religions.Where(r =>
                        (searchColumn == "Religion_Id" && r.Religion_Id.ToString().Contains(searchText)) ||
                        (searchColumn == "Religion" && MatchSearch(r.Religion, searchText, searchType)) ||
                        (searchColumn == "Religion_L" && MatchSearch(r.Religion_L, searchText, searchType)) ||
                        (searchColumn == "CreatedBy" && MatchSearch(r.CreatedBy, searchText, searchType)) ||
                        (searchColumn == "ModifiedBy" && MatchSearch(r.ModifiedBy, searchText, searchType))
                    ).ToList();
                }

                // Sorting
                if (!string.IsNullOrEmpty(sortColumn) && typeof(ReligionMaster).GetProperty(sortColumn) != null)
                {
                    religions = sortOrder == "asc"
                        ? religions.OrderBy(r => typeof(ReligionMaster).GetProperty(sortColumn).GetValue(r)).ToList()
                        : religions.OrderByDescending(r => typeof(ReligionMaster).GetProperty(sortColumn).GetValue(r)).ToList();
                }

                // Apply Pagination
                var pagedList = religions.ToPagedList(page, pageSize);
                return PartialView("_ListPartial", pagedList);
            }




            [HttpPost]
            public async Task<IActionResult> EditPartial(ReligionMaster model)
            {
                if (model.Religion_Id == null || model.Religion_Id == 0)
                {
                    return PartialView("_ReligionFormPartial", model); // Return form with errors
                }

                try
                {
                    //int optype = 2; // Assuming '2' is the operation type for updates
                    var success = await _spService.UpdateReligionAsync(model.Religion_Id, model.Religion, model.Religion_L);

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

        #endregion

    }
}

