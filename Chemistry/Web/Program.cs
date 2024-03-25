using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using DataAcces.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.Services.Concret;
using HomeService = Web.Services.Concret.HomeService;
using IHomeService = Web.Services.Abstract.IHomeService;
using Core.Utilities;
using DataAcces;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.Services.Concrete;
using IQuizCategoryService = Web.Services.Abstract.IQuizCategoryService;
using QuizCategoryService = Web.Services.Concret.QuizCategoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAcces")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 7;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.User.RequireUniqueEmail = false;


    //options.RE

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;
})
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

#region Repositories
builder.Services.AddScoped<IPersonInfoRepository, PersonInfoRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IHomeSliderRepository, HomeSliderRepository>();
builder.Services.AddScoped<IVideoLessonRepository, VideoLessonRepository>();
builder.Services.AddScoped<ICurseInfoRepository, CurseInfoRepository>();
builder.Services.AddScoped<IQuizzesRepository, QuizRepository>();
builder.Services.AddScoped<IQuizCategoryRepository,QuizCategoryRepository>();
builder.Services.AddScoped<IQuizAnswersRepository,QuizAnswersRepository>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();


#endregion

#region Services
builder.Services.AddScoped<IAccountService, AccountService>();

//builder.Services.AddScoped<IUrlHelper, UrlHelper>();

builder.Services.AddScoped<IUrlHelper>(factory =>
{
    var actionContext = factory.GetService<IActionContextAccessor>()
                                   .ActionContext;
    return new UrlHelper(actionContext);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IUsersService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IQuizCategoryService, QuizCategoryService>();
builder.Services.AddScoped<Web.Services.Abstract.IQuizService, Web.Services.Concret.QuizService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IQuizService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.QuizService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IQuizCategoryService,Web.Areas.chemistry_Vafa_admin.Services.Concrete.QuizCategoryService>();
builder.Services.AddScoped<Web.Services.Abstract.IVideoLessonService, Web.Services.Concret.VideoLessonService>();
builder.Services.AddScoped<Web.Services.Abstract.INewsService, Web.Services.Concret.NewsService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IPersonInfoService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.PersonInfoService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IStudentsService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.StudentsService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.INewsService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.NewsService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IHomeSliderService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.HomeSliderService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IVideoLessonService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.VideoLessonService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.ICurseInfoService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.CurseInfoService>();
builder.Services.AddScoped<Web.Areas.chemistry_Vafa_admin.Services.Abstract.IResourceService, Web.Areas.chemistry_Vafa_admin.Services.Concrete.ResourceService>();

#endregion


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllerRoute(
    name: "areas",
    pattern:"{area:exists}/{controller=dashboard}/{action=index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

var scopFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(roleManager, userManager);
}

app.Run();
