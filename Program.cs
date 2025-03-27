using Microsoft.EntityFrameworkCore;
using Pryce_MVC.Data;
using Pryce_MVC.Repositories;
using Pryce_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PegaConnection")));

// Register Repository
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<ISPRepository, SPRepository>();
builder.Services.AddScoped<ISPService, SPService>();


var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
