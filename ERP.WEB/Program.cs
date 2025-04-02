using ERP.DataAccess.DBContext;
using ERP.Infrastructure.Interfaces;
using ERP.Repositories.Services;
using ERP.WEB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBcontext")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISpService, SpService>();
builder.Services.AddScoped<IDatabaseBackupService, DatabaseBackupService>();
// Add memory cache (needed for session)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
    options.Cookie.HttpOnly = true; // Set HttpOnly cookie
    options.Cookie.IsEssential = true; // Mark session cookie as essential
});


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}")
    .WithStaticAssets();
app.MapGet("/backup-database", async (IDatabaseBackupService backupService) =>
{
    string backupFileName = $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
    string backupFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "DatabaseBackups");

    if (!Directory.Exists(backupFolderPath))
    {
        Directory.CreateDirectory(backupFolderPath);
    }
    string backupFilePath = Path.Combine(backupFolderPath, backupFileName);

    bool result = await backupService.BackupDatabaseAsync(backupFilePath);

    return result ? Results.Ok($"Backup successful! File: {backupFilePath}") : Results.BadRequest("Backup failed.");
});
app.Run();
