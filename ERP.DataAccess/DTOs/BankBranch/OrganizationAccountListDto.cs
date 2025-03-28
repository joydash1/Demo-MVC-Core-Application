using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.BankBranch
{
    public record OrganizationAccountListDto(int Id,string OrganizatioName,string BankName,string BranchName, string BankAccountNo);
}
