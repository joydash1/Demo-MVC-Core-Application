using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.Buyer
{
    public record IndianBuyerDto(int Id,string BuyerName,string ShopName,string Address,string Website,string TINNumber);
}
