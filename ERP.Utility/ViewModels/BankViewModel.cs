using ERP.DataAccess.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Utility.ViewModels
{
    public class BankViewModel
    {
        public Bank Bank { get; set; } //= new Organization();
        public IEnumerable<Bank> BankList { get; set; } = new List<Bank>();
    }
}
