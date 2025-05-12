using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class CNFCompnay
    {
        [Key]
        public int Id { get; set; }
        public required string CNFCompnayName { get; set; }
        public required int BorderId { get; set; }

        public required int EntryUserId { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public required bool IsActive { get; set; }
    }
}
