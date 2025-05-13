using ERP.DataAccess.Domains;

namespace ERP.Utility.ViewModels
{
    public class BankViewModel
    {
        public Bank Bank { get; set; } //= new Organization();
        public IEnumerable<Bank> BankList { get; set; } = new List<Bank>();
    }
}