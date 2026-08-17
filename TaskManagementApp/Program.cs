using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Logging;
using TaskManagementApp.LogModels;
using TaskManagementApp.Models;
using TaskManagementApp.Repositories;
using TaskManagementApp.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

// Register Entity Framework Core
builder.Services.AddDbContext<TaskManagementContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<LogContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICustomLogger, CustomLogger>();

var app = builder.Build();

// Configure HTTP request pipeline
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
