using System.Globalization;
namespace ERP.Utility.Helpers
{
    public static class ReportHelpers
    {
        static string CompanyName = "United Corporate Advisory Service Ltd.";
        static string CompanyAddress = "Bijoy Nagar, Kakrail, Dhaka";
        static string MobileNo = "+88015364578, +8801345759842";
        static string CompanyLogo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/user/avatar1.jpg");
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
        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> sourceTask)
        {
            var result = await sourceTask;
            return result.ToList();
        }
    }
}
