using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pryce_MVC.Models;

namespace Pryce_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<Pryce_Master_Module> MasterModules { get; set; }
        public DbSet<Pryce_Object_Module> ObjectModules { get; set; }
        public DbSet<Country_Master> Country_Master { get; set; }
        public DbSet<Religion_Master> Religion_Master { get; set; }
        public DbSet<ReligionMaster> ReligionMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pryce_Master_Module>()
                .HasMany(m => m.ObjectModules)
                .WithOne(o => o.MasterModule)
                .HasForeignKey(o => o.Module_id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Religion_Master>()
            .HasOne(r => r.Country)
            .WithMany(c => c.Religions)
            .HasForeignKey(r => r.Country_Id)
            .OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            _logger.LogInformation("Saving changes to database...");
            return base.SaveChanges();
        }
    }
}
