
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Repositories.Interfaces;
using SchoolManagementSystem.Repositories.Implementation;
using SchoolManagementSystem.Services.Interfaces;
using SchoolManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);
//mvc
builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            _ => "مقدار ارسال شده معتبر نیست.");
    });

// Add services to the container.

//database
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // فعلاً برای محیط توسعه ساده‌تر می‌کنیم
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<SchoolDbContext>()
.AddDefaultTokenProviders();
//repositori
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
//services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<ICaptchaService, CaptchaService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// کدهای مربوط به MiddlewareIdentity Seeder

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<ApplicationUser>>();

    await IdentitySeeder.SeedAsync(
        roleManager,
        userManager);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
