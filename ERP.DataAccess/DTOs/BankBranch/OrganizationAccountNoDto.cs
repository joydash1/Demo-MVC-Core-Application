namespace ERP.DataAccess.DTOs.BankBranch
{
    public record OrganizationAccountNoDto(int Id, int BankId, int BranchId, string BankAccountNo, string BranchName);
}