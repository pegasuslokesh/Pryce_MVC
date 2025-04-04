using Microsoft.AspNetCore.Mvc;
using Pryce_MVC.Models;
using Pryce_MVC.Repositories;
using System.Diagnostics;

namespace Pryce_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMenuRepository _menuRepository;
    private readonly ISPRepository _ISPRepository;
    public HomeController(ISPRepository ispRepository, ILogger<HomeController> logger)

    {
        _ISPRepository = ispRepository;
        _logger = logger;
    }

    //public async Task<IActionResult> Index(int optype = 1)

    //{
    //    var masterModules = await _ISPRepository.sp_IT_ModuleMaster_SelectRow(0, null, optype);

    //    if (masterModules == null || !masterModules.Any())
    //    {
    //        _logger.LogWarning("No modules found from stored procedure.");
    //    }

    //    ViewData["MasterModules"] = masterModules; // ? Pass data globally
    //    return View();
    //}

    public async Task<IActionResult> Index(int optype = 1)
    {
        var masterModules = await _ISPRepository.sp_IT_ModuleMaster_SelectRow(0, null, optype);
        var objectModules = await _ISPRepository.sp_IT_ObjectEntry_SelectRow(0, null, optype);
        var moduleObjects = await _ISPRepository.sp_IT_App_Mod_Object_SelectRow(0, 1); // Fetch module-object relationships

        if (masterModules == null || !masterModules.Any())
            _logger.LogWarning("No modules found from stored procedure.");

        if (objectModules == null || !objectModules.Any())
            _logger.LogWarning("No objects found from stored procedure.");



        ViewBag.MasterModules = masterModules; // Store Modules
        ViewBag.ObjectEntries = objectModules; // Store Objects
        ViewBag.ModuleObjects = moduleObjects; // Store module-object relationships

        return View();
    }


    //    public async Task<IActionResult> Index(int optype = 1)
    //    {
    //        var masterModules = await _ISPRepository.sp_IT_ModuleMaster_SelectRow(0, null, optype);
    //        var objectEntries = await _ISPRepository.sp_IT_ObjectEntry_SelectRow(0, null, optype);
    //        var moduleObjectRelations = await _ISPRepository.sp_IT_App_Mod_Object_SelectRow(0, optype);

    //        if (masterModules == null || !masterModules.Any())
    //            _logger.LogWarning("No modules found from stored procedure.");

    //        if (objectEntries == null || !objectEntries.Any())
    //            _logger.LogWarning("No objects found from stored procedure.");

    //        if (moduleObjectRelations == null || !moduleObjectRelations.Any())
    //            _logger.LogWarning("No module-object relations found from stored procedure.");

    //        // Create a dictionary to map Module_Id to its objects
    //        var moduleObjectMap = moduleObjectRelations
    //    .GroupBy(r => r.Module_Id)
    //    .ToDictionary(g => g.Key, g => objectEntries.Where(o => g.Any(r => r.Object_Id == o.Object_Id)).ToList());

    //ViewBag.MasterModules = masterModules;
    //ViewBag.ObjectEntries = moduleObjectMap;


    //        return View();
    //    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
