using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.Seller
{
    public record BangladeshiSellerDto(int Id,string SellerName, string ShopName,string Address,string Website,string TINNumber,string MobileNo,string EmailAddress);
}
