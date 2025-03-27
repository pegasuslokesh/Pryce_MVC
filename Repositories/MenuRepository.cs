using Microsoft.EntityFrameworkCore;
using Pryce_MVC.Data;
using Pryce_MVC.Models;

namespace Pryce_MVC.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MenuRepository> _logger;

        public MenuRepository(ApplicationDbContext context, ILogger<MenuRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Pryce_Master_Module>> GetMasterModulesAsync()
        {
            _logger.LogInformation("Fetching Master Modules from database...");

            //var masterModules = await _context.MasterModules
            //    .Include(m => m.ObjectModules)
            //    .Include(m => m.SubModules)
            //    .ThenInclude(sm => sm.SubModules)
            //    .ToListAsync();
            var masterModules = await _context.MasterModules
                 .Include(m => m.SubModules) // Load submodules
                 .Include(m => m.ObjectModules) // Load object modules for main modules
                 .ToListAsync();

            if (!masterModules.Any())
            {
                _logger.LogWarning("⚠️ No master modules found in the database.");
            }
            else
            {
                foreach (var module in masterModules)
                {
                    _logger.LogInformation($"📌 Master Module: {module.Module_name}");

                    foreach (var objectModule in module.ObjectModules)
                    {
                        _logger.LogInformation($"   📍 Object Module: {objectModule.ObjectModule_name}");
                    }
                    foreach (var subModule in module.SubModules)
                    {
                        _logger.LogInformation($"   🔹 Sub Module: {subModule.Module_name}");
                    }
                }
            }

            return masterModules;
        }
    }
}
