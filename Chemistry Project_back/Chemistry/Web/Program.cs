using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using DataAcces.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.Services.Concret;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.Services.Concrete;
using PersonInfoService = Web.Services.Concret.PersonInfoService;
using IPersonInfoService = Web.Services.Abstract.IPersonInfoService;
using Core.Utilities;
using DataAcces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAcces")));
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>();

#region Repositories
builder.Services.AddScoped<IPersonInfoRepository, PersonInfoRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IHomeSliderRepository, HomeSliderRepository>();

#endregion
#region Services
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddScoped<IPersonInfoService, PersonInfoService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IPersonInfoService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.PersonInfoService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IStudentsService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.StudentsService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.INewsService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.NewsService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IHomeSliderService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.HomeSliderService>();

#endregion


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
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllerRoute(
    name: "areas",
    pattern:"{area:exists}/{controller=dashboard}/{action=index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

var scopFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(roleManager, userManager);
}

app.Run();
