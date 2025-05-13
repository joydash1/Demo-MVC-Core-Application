using ERP.DataAccess.DBContext;
using ERP.Infrastructure.Interfaces;
using ERP.Repositories.Services;
using ERP.Utility.Helpers;
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
builder.Services.AddScoped<IExcelBulkInsertService, ExcelBulkInsertService>();
builder.Services.AddScoped<ReportService>();

// Add memory cache (needed for session)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

        // ✔️ Allow reading token from cookie
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Read the token from the cookie if not in Authorization header
                var token = context.HttpContext.Request.Cookies["jwtToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}")
    .WithStaticAssets();

#region DB BackUp

//app.MapGet("/backup-database", async (IDatabaseBackupService backupService) =>
//{
//    string backupFileName = $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
//    string backupFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "DatabaseBackups");

//    if (!Directory.Exists(backupFolderPath))
//    {
//        Directory.CreateDirectory(backupFolderPath);
//    }
//    string backupFilePath = Path.Combine(backupFolderPath, backupFileName);

//    bool result = await backupService.BackupDatabaseAsync(backupFilePath);

//    return result ? Results.Ok($"Backup successful! File: {backupFilePath}") : Results.BadRequest("Backup failed.");
//});

#endregion DB BackUp

app.Run();