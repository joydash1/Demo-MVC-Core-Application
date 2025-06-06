using System.Globalization;

namespace ERP.Utility.Helpers
{
    public static class ReportHelpers
    {
        private static string CompanyName = "Firoz Enterprise.";
        private static string CompanyAddress = "Head office : Firoz Plaza, 3rd Floor Bhomra Land port, Bhomra Satkhira-9400";
        private static string MobileNo = "01705230260";
        private static string Website = "www.firozenterprise.com";
        private static string Email = "firozenterprise02gmail.com";

        public static DateTime? TryParseDate(string date)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(date))
                    return null;

                if (DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                    return parsedDate;

                return null;
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> sourceTask)
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