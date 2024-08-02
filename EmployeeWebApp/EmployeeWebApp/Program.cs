using EmployeeWebApp.Business;
using EmployeeWebApp.Data;
using EmployeeWebApp.DataAccess;
using EmployeeWebApp.Mappings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//connected Db context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("localDb")));

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Business
builder.Services.AddScoped<IEmployeeDataAccess, EmployeeDataAccess>();

// DataAccess
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


var app = builder.Build();

// Use custom exception handling middleware
app.UseMiddleware<EmployeeWebApp.Middleware.ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();
