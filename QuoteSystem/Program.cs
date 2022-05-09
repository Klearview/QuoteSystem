using FRCScouting_API.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuoteSystem.Models;
using QuoteSystem.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Get Api Settings
services.Configure<ApiSettings>(configuration.GetSection("Api"));

// Bind Api Settings
var apiSettings = new ApiSettings();
configuration.GetSection("Api").Bind(apiSettings);

// Sql Server connection string
var connectionString = apiSettings.AppDataContext!
    .Replace("{AppDataContextCredentials}", apiSettings.AppDataContextCredentials);

// Add DB Context
services.AddDbContext<AppDataContext>(options =>
{
    options.UseSqlServer(connectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
services.AddScoped<IAppDataRepository, AppDataRepository>();

services.AddControllersWithViews();

services.AddRazorPages().AddRazorRuntimeCompilation();

// Applicaton Insights
services.AddApplicationInsightsTelemetry();

// AUTH
services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = configuration["Authentication:Google:ClientId"];
        options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseCookiePolicy();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
