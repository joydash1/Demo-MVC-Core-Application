using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.BankBranch
{
    public record BranchByBankDto(int BranchId,string BranchName, int RoutingNo);
}
