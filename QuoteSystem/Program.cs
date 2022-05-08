using FRCScouting_API.Models;
using Microsoft.EntityFrameworkCore;
using QuoteSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Get Api Settings
var configuration = builder.Configuration;
builder.Services.Configure<ApiSettings>(configuration.GetSection("Api"));

// Bind Api Settings
var apiSettings = new ApiSettings();
configuration.GetSection("Api").Bind(apiSettings);

// Sql Server connection string
var connectionString = apiSettings.AppDataContext!
    .Replace("{AppDataContextCredentials}", apiSettings.AppDataContextCredentials);

// Add DB Context
builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseSqlServer(connectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IAppDataRepository, AppDataRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Applicaton Insights
builder.Services.AddApplicationInsightsTelemetry();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
