using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class CollectionMode
    {
        [Key]
        public int Id { get; set; }
        public required string CollectionModeName { get; set; }
        public required int IsActive { get; set; }
    }
}
