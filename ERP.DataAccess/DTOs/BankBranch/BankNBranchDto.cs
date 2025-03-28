using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.BankBranch
{
    public record BankNBranchDto(int Id,string BranchName,int RoutingNo, string BankName);
}
