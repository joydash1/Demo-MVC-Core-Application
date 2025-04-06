using System.Globalization;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
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

        public static FileResult GenerateReport<T>(string reportFileName, string outputFileName, IEnumerable<T> data, string reportType = "pdf")
        {
            // Convert IEnumerable<T> to DataTable
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                    values[i] = Props[i].GetValue(item, null);
                dataTable.Rows.Add(values);
            }

            // Load RDLC
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", reportFileName);
            if (!File.Exists(reportPath))
            {
                throw new FileNotFoundException($"Report file '{reportFileName}' not found at path '{reportPath}'");
            }

            LocalReport report = new LocalReport();
            report.LoadReportDefinition(System.IO.File.OpenRead(reportPath));
            report.DataSources.Add(new ReportDataSource("DataSet1", dataTable)); // Ensure "DataSet1" matches your RDLC dataset name

            // Render
            string mimeType = reportType.ToUpper() == "EXCEL" ? "application/vnd.ms-excel" : "application/pdf";
            string extension = reportType.ToUpper() == "EXCEL" ? "xlsx" : "pdf";
            byte[] bytes = report.Render(reportType.ToUpper());  // Ensure correct report type

            return new FileContentResult(bytes, mimeType)
            {
                FileDownloadName = $"{outputFileName}.{extension}"
            };
        }

    }
}
