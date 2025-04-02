using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.LC_Open
{
    public record LCTransactionDto(
        int Id,
        string OpeningDate,
        string LCNumber,
        int OrganizationBankId,
        int OrganizationBranchId,
        string OrganizationAccountNo,
        int InsuranceId,
        decimal ProductWeightTon,
        decimal AmountPerTon,
        decimal TotalLCAmount,
        int ProductId,
        string ProductName,
        string ShopName,
        int BorderId,
        int ImporterId,
        int ExporterId
    );
}
