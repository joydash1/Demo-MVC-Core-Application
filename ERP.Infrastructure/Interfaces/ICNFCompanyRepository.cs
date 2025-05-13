using ERP.DataAccess.Domains;

namespace ERP.Infrastructure.Interfaces
{
    public interface ICNFCompanyRepository : IGenericRepository<CNFCompnay>
    {
        void Update(CNFCompnay cNFCompnay);

        void UpdateRange(IEnumerable<CNFCompnay> cNFCompnays);
    }
}