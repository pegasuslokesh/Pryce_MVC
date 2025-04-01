using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pryce_MVC.Data;
using Pryce_MVC.Models;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc;
using System.Linq.Dynamic.Core;
using Pryce_MVC.Services;

namespace Pryce_MVC.Controllers
{
    public class Religion_MasterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISPService _spService;

        public Religion_MasterController(ISPService spService)
        {
            _spService = spService;
        }
        //code for religian master display grid insert new recode and update and delete 
        //Created by jitendra singh rao 29/03/2025 
        #region
        public async Task<IActionResult> Index()
        {
            //var religions = await _spService.GetReligionDataAsync(0, null, optype);
            return View();
        }
        // GET: Religion_Master/Details/5



        public async Task<IActionResult> ListPartial(int page = 1, int pageSize = 10, string sortColumn = "Religion", string sortOrder = "asc", string searchColumn = "", string searchType = "", string searchText = "", int optype = 2)
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
            int optype = 4;
            var religions = await _spService.GetReligionDataAsync(id, null, optype);

            // Ensure you return a single ReligionMaster object, not a list
            var religion = religions.FirstOrDefault();

            if (religion == null)
            {
                return NotFound(); // Handle case where no religion is found
            }

            return PartialView("_NewPartial", religion);
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
            if (model.Religion_Id==null ||model.Religion_Id==0)
            {
                return PartialView("_ReligionFormPartial", model); // Return form with errors
            }

            try
            {
                //int optype = 2; // Assuming '2' is the operation type for updates
                var success = await _spService.UpdateReligionAsync(model.Religion_Id, model.Religion, model.Religion_L);

                if (success>0)
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
                int result = await _spService.DeleteReligionAsync( id, isActive, modifiedBy, modifiedDate);

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

      
    }
}
#endregion
#region
//Old code for getting data from table not use Store_Procedure 
//It's commented on 29/03/2025 by jitendra singh rao

//[HttpPost]
//public IActionResult Create(Religion_Master religion)
//{
//    if (religion.Country_Id != 0 && religion.Religion_Name != "" && religion.Religion_Name != null)
//    {
//        _context.Religion_Master.Add(religion);
//        _context.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    ViewData["Country_Id"] = new SelectList(_context.Country_Master, "Country_Id", "Country_Name", religion.Country_Id);
//    return View("Index");
//}
//This is a Index page for ReligionMaster for get data from sp created by 
//public async Task<IActionResult> Index(int optype = 2)            
//{
//    //var religions = await _spService.GetReligionDataAsync(0, null, optype);
//    return View(religions);
//}
//public async Task<IActionResult> Details(int? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var religion_Master = await _context.Religion_Master
//        .Include(r => r.Country)
//        .FirstOrDefaultAsync(m => m.Religion_Id == id);
//    if (religion_Master == null)
//    {
//        return NotFound();
//    }

//    return View(religion_Master);
//}

// GET: Religion_Master/Create
//public IActionResult Index()
//{
//    var activeReligions = _context.Religion_Master
//        .Include(r => r.Country)

//        .ToList();

//    var countries = _context.Country_Master.ToList();

//    ViewData["Country_Id"] = new SelectList(countries, "CountryId", "Country_Name");

//    return View(activeReligions);
//}
//private static IQueryable<Religion_Master> ApplySorting(IQueryable<Religion_Master> query, string sortColumn, string sortOrder)
//{
//    if (string.IsNullOrEmpty(sortColumn))
//    {
//        return query; // Return unsorted query
//    }

//    string orderQuery = $"{sortColumn} {sortOrder}"; // "Religion_Name asc" or "Religion_Name desc"

//    return query.OrderBy(orderQuery); // Dynamic sorting
//}

//private static Expression<Func<Religion_Master, object>> GetSortProperty(string sortColumn)
//{
//    if (sortColumn.Equals("Religion_Name", StringComparison.OrdinalIgnoreCase))
//    {
//        return r => r.Religion_Name;
//    }
//    else if (sortColumn.Equals("Country_Name", StringComparison.OrdinalIgnoreCase))
//    {
//        return r => r.Country != null ? r.Country.Country_Name : "";
//    }
//    else
//    {
//        // Default to Religion_Name if the column is not recognized.
//        return r => r.Religion_Name;
//    }
//}
//public IActionResult InactivePartial()
//{
//    // Example: Fetch inactive records (replace with your logic)
//    var inactiveReligions = _context.Religion_Master.Include(r => r.Country).Where(r => r.Religion_Id > 1000).ToList();
//    return PartialView("_InactivePartial", inactiveReligions);
//}
// GET: Religion_Master/Delete/5
//public async Task<IActionResult> Delete(long? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var religion_Master = await _context.Religion_Master
//        .Include(r => r.Country)
//        .FirstOrDefaultAsync(m => m.Religion_Id == id);
//    if (religion_Master == null)
//    {
//        return NotFound();
//    }

//    return View(religion_Master);
//}
//public Religion_MasterController(ApplicationDbContext context)
//{
//    _context = context;
//}

// GET: Religion_Master
//       public IActionResult Index()

//       {
//           var religions = _context.Religion_Master.Include(r => r.Country).ToList();
//           var countries = _context.Country_Master.ToList();

//           // Ensure correct SelectList initialization
//           ViewData["Country_Id"] = new SelectList(
//    countries, // Pass the list directly without using Select()
//    "CountryId", // Make sure these match the actual property names
//    "Country_Name"
//);

//    return View(religions);
//}
//public IActionResult Search(string field, string condition, string text)
//{
//    var religions = _context.Religion_Master.AsQueryable();

//    if (!string.IsNullOrEmpty(text))
//    {
//        switch (field)
//        {
//            case "Religion Name":
//                religions = condition switch
//                {
//                    "Contains" => religions.Where(r => r.Religion_Name.Contains(text)),
//                    "Equals" => religions.Where(r => r.Religion_Name == text),
//                    "Starts With" => religions.Where(r => r.Religion_Name.StartsWith(text)),
//                    "Ends With" => religions.Where(r => r.Religion_Name.EndsWith(text)),
//                    _ => religions
//                };
//                break;

//            case "Country Name":
//                religions = condition switch
//                {
//                    "Contains" => religions.Where(r => r.Country.Country_Name.Contains(text)),
//                    "Equals" => religions.Where(r => r.Country.Country_Name == text),
//                    "Starts With" => religions.Where(r => r.Country.Country_Name.StartsWith(text)),
//                    "Ends With" => religions.Where(r => r.Country.Country_Name.EndsWith(text)),
//                    _ => religions
//                };
//                break;
//        }
//    }

//    // Log the generated SQL query
//    Console.WriteLine(religions.ToQueryString());

//    return PartialView("ListPartial", religions.ToList().ToPagedList(1, 10));
//}

//private bool Religion_MasterExists(long id)
//{
//    return _context.Religion_Master.Any(e => e.Religion_Id == id);
//}
#endregion