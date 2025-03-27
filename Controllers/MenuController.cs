using Microsoft.AspNetCore.Mvc;
using Pryce_MVC.Repositories;

namespace Pryce_MVC.Controllers
{

    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ILogger<MenuController> _logger;

        public MenuController(IMenuRepository menuRepository, ILogger<MenuController> logger)
        {
            _menuRepository = menuRepository;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var masterModules = await _menuRepository.GetMasterModulesAsync();
            ViewBag.MasterModules = masterModules; // ✅ Store in ViewBag
            return View(masterModules);
        }
    }

}
