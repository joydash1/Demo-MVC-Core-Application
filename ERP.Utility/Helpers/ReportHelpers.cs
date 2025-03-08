using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Utility.Helpers
{
    public class ReportHelpers
    {
        public static DateTime FormatDateToString(string date)
        {
            try
            {
                DateTime parsedDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return parsedDate; // Example: "05 Feb 2025"
            }
            catch (FormatException)
            {
                return DateTime.Now;
            }
        }
    }
}
