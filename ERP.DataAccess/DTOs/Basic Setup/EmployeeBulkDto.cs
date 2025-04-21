using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.Basic_Setup
{
    public class EmployeeBulkDto
    {
        public string EmployeeName { get; set; }
        public int? EmployeeAge { get; set; }
        public string EmployeeGender { get; set; }
        public string EmployeeMobile { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeCountry { get; set; }
        public string EmployeeDesignation { get; set; }
        public string EmployeeBloodGroup { get; set; }
        public decimal? EmployeeSalary { get; set; }
    }
}
