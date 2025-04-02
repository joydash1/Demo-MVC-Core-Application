using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.DTOs.Basic_Setup
{
    public record  ProductDto (int Id, int CategoryId, string ProductName,string? CategoryName);
    
}
