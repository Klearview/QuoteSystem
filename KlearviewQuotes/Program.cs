using DinkToPdf;
using DinkToPdf.Contracts;
using KlearviewQuotes.Data;
using KlearviewQuotes.Models;
using KlearviewQuotes.Services;
using KlearviewQuotes.Services.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get Api Settings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("Api"));

// Database Connections
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IdentityDataContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AppDataContext>(options => 
    options.UseSqlServer(connectionString)
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// AUTH
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>();
builder.Services.AddControllersWithViews();

// Application Insights
builder.Services.AddApplicationInsightsTelemetry();

// PDF Converter Backend Service
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Add Node Services
builder.Services.AddNodeServices();

// Services
builder.Services.AddScoped<IAppDataRepository, AppDataRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPDFService, PDFService>();

// Runtime Compilation
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.Run();
