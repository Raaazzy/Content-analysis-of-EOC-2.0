using EOC_2_0;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using EOC_2_0.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContent>(options =>
    options.UseSqlServer(connection));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();