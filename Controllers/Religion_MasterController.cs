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


        public async Task<IActionResult> Index(int optype = 1)
        
        
        {
            var religions = await _spService.GetReligionDataAsync(0, null, optype);
            return View(religions);
        }





        // GET: Religion_Master/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var religion_Master = await _context.Religion_Master
                .Include(r => r.Country)
                .FirstOrDefaultAsync(m => m.Religion_Id == id);
            if (religion_Master == null)
            {
                return NotFound();
            }

            return View(religion_Master);
        }


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
        public async Task<IActionResult> ListPartial(int page = 1, int pageSize = 10, string sortColumn = "Religion_Name", string sortOrder = "asc", string searchColumn = "", string searchType = "", string searchText = "", int optype = 1)
        {
            // Fetch data using stored procedure service
            var religions = await _spService.GetReligionDataAsync(0, null, optype);

            // Apply Searching
            if (!string.IsNullOrEmpty(searchColumn) && !string.IsNullOrEmpty(searchText))
            {
                religions = religions.Where(r =>
                    searchColumn == "Religion_Name" && searchType == "contains" ? r.Religion.Contains(searchText) :
                    searchColumn == "Religion_Name" && searchType == "equals" ? r.Religion == searchText :
                    searchColumn == "Religion_Name" && searchType == "startswith" ? r.Religion.StartsWith(searchText) :
                    searchColumn == "Religion_Name" && searchType == "endswith" ? r.Religion.EndsWith(searchText) :
                    searchColumn == "Religion_Name_Local" && searchType == "contains" ? r.Religion_L.Contains(searchText) :
                    searchColumn == "Religion_Name_Local" && searchType == "equals" ? r.Religion_L == searchText :
                    searchColumn == "Religion_Name_Local" && searchType == "startswith" ? r.Religion_L.StartsWith(searchText) :
                    searchColumn == "Religion_Name_Local" && searchType == "endswith" ? r.Religion_L.EndsWith(searchText) :
                    false
                ).ToList();
            }

            if (!string.IsNullOrEmpty(sortColumn) && typeof(ReligionMaster).GetProperty(sortColumn) != null)
            {
                if (sortOrder == "asc")
                    religions = religions.OrderBy(r => typeof(ReligionMaster).GetProperty(sortColumn).GetValue(r)).ToList();
                else
                    religions = religions.OrderByDescending(r => typeof(ReligionMaster).GetProperty(sortColumn).GetValue(r)).ToList();
            }


            // Apply Pagination
            var pagedList = religions.ToPagedList(page, pageSize);
            return PartialView("_ListPartial", pagedList);
        }


        private static IQueryable<Religion_Master> ApplySorting(IQueryable<Religion_Master> query, string sortColumn, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortColumn))
            {
                return query; // Return unsorted query
            }

            string orderQuery = $"{sortColumn} {sortOrder}"; // "Religion_Name asc" or "Religion_Name desc"

            return query.OrderBy(orderQuery); // Dynamic sorting
        }

        private static Expression<Func<Religion_Master, object>> GetSortProperty(string sortColumn)
        {
            if (sortColumn.Equals("Religion_Name", StringComparison.OrdinalIgnoreCase))
            {
                return r => r.Religion_Name;
            }
            else if (sortColumn.Equals("Country_Name", StringComparison.OrdinalIgnoreCase))
            {
                return r => r.Country != null ? r.Country.Country_Name : "";
            }
            else
            {
                // Default to Religion_Name if the column is not recognized.
                return r => r.Religion_Name;
            }
        }



        public IActionResult NewPartial()
        {
            var countries = _context.Country_Master.ToList();
            var selectListItems = countries.Select(c => new SelectListItem
            {
                Value = c.CountryId.ToString(), // Use Country_Id as the value
                Text = c.Country_Name // Use Country_Name as the text
            }).ToList();

            ViewData["Country_Id"] = new SelectList(selectListItems, "Value", "Text");
            return PartialView("_NewPartial");
        }

        public IActionResult EditPartial(long id)
        {
            var religion = _context.Religion_Master.Find(id);

            if (religion != null)
            {
                var countries = _context.Country_Master.ToList();
                var selectListItems = countries.Select(c => new SelectListItem
                {
                    Value = c.CountryId.ToString(),
                    Text = c.Country_Name
                }).ToList();

                ViewData["Country_Id"] = new SelectList(selectListItems, "Value", "Text", religion.Country_Id.ToString()); // Pass selected value
                return PartialView("_NewPartial", religion);
            }

            return NotFound();
        }

            // Handle the case where the religion is not found (return NotFound, for example)
           
        public IActionResult InactivePartial()
        {
            // Example: Fetch inactive records (replace with your logic)
            var inactiveReligions = _context.Religion_Master.Include(r => r.Country).Where(r => r.Religion_Id > 1000).ToList();
            return PartialView("_InactivePartial", inactiveReligions);
        }

        [HttpPost]
        public IActionResult Create(Religion_Master religion)
        {
            if (religion.Country_Id!=0 && religion.Religion_Name!="" && religion.Religion_Name != null)
            {
                _context.Religion_Master.Add(religion);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["Country_Id"] = new SelectList(_context.Country_Master, "Country_Id", "Country_Name", religion.Country_Id);
            return View("Index");
        }

        [HttpPost]
        public IActionResult Edit(Religion_Master religion)
        {
            if (religion.Country_Id != 0 && religion.Religion_Name != "" && religion.Religion_Name != null)
            {
                _context.Religion_Master.Update(religion);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["Country_Id"] = new SelectList(_context.Country_Master, "Country_Id", "Country_Name", religion.Country_Id);
            return View("Index");
        }

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

        // POST: Religion_Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var religion_Master = await _context.Religion_Master.FindAsync(id);
            if (religion_Master != null)
            {
                _context.Religion_Master.Remove(religion_Master);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Search(string field, string condition, string text)
        {
            var religions = _context.Religion_Master.AsQueryable();

            if (!string.IsNullOrEmpty(text))
            {
                switch (field)
                {
                    case "Religion Name":
                        religions = condition switch
                        {
                            "Contains" => religions.Where(r => r.Religion_Name.Contains(text)),
                            "Equals" => religions.Where(r => r.Religion_Name == text),
                            "Starts With" => religions.Where(r => r.Religion_Name.StartsWith(text)),
                            "Ends With" => religions.Where(r => r.Religion_Name.EndsWith(text)),
                            _ => religions
                        };
                        break;

                    case "Country Name":
                        religions = condition switch
                        {
                            "Contains" => religions.Where(r => r.Country.Country_Name.Contains(text)),
                            "Equals" => religions.Where(r => r.Country.Country_Name == text),
                            "Starts With" => religions.Where(r => r.Country.Country_Name.StartsWith(text)),
                            "Ends With" => religions.Where(r => r.Country.Country_Name.EndsWith(text)),
                            _ => religions
                        };
                        break;
                }
            }

            // Log the generated SQL query
            Console.WriteLine(religions.ToQueryString());

            return PartialView("ListPartial", religions.ToList().ToPagedList(1, 10));
        }

        private bool Religion_MasterExists(long id)
        {
            return _context.Religion_Master.Any(e => e.Religion_Id == id);
        }
    }
}
