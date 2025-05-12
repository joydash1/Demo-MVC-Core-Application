using System.Globalization;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace ERP.Utility.Helpers
{
    public static class ReportHelpers
    {
        static string CompanyName = "Firoz Enterprise.";
        static string CompanyAddress = "Head office : Firoz Plaza, 3rd Floor Bhomra Land port, Bhomra Satkhira-9400";
        static string MobileNo = "01705230260";
        static string Website = "www.firozenterprise.com";
        static string Email = "firozenterprise02gmail.com";
        public static DateTime FormatDateToString(string date)
        {
            try
            {
                //DateTime parsedDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime parsedDate = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                return parsedDate;
            }
            catch (FormatException)
            {
                return DateTime.Now;
            }
        }
        public static  async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> sourceTask)
        {
            var result = await sourceTask;
            return result.ToList();
        }

        public static Dictionary<string, string> GetCompanyHeader()
        {
            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "user", "avatar1.jpg");
            var logoBase64 = File.Exists(logoPath) ?
                Convert.ToBase64String(File.ReadAllBytes(logoPath)) :
                string.Empty;

            return new Dictionary<string, string>
            {
                { "CompanyLogo", logoBase64 },
                { "CompanyName", CompanyName },
                { "CompanyAddress", CompanyAddress },
                { "Mobile", MobileNo },
                { "Website", Website },
                { "Email", Email }
            };
        }
    }
}
