using TabCreator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabCreator.Models;
using TabCreator.Services.Interfaces;
using TabCreator.Services;
using TabCreator.Repositories.Interfaces;
using TabCreator.Repositories;
using TCG_Collector.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TabCreatorContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TabCreator")));

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

builder.Services.AddScoped<IChordsRepository, ChordsRepository>();
builder.Services.AddScoped<IChordsService, ChordsService>();

builder.Services.AddScoped<ISheetRepository, SheetRepository>();
builder.Services.AddScoped<ISheetService, SheetService>();

builder.Services.AddScoped<ITablatureRepository, TablatureRepository>();
builder.Services.AddScoped<ITablatureService, TablatureService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
