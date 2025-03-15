using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataAccess.Domains
{
    public class Organization
    {
        [Key]
        public int ID { get; set; }
        public required string OrganizatioName { get; set; }
        public required string OwnerName { get; set; }
        public required string OrganizationAddress { get; set; }
        public required string MobileNo { get; set; }
        public required string TinNumber { get; set; }
        public  string? WebsiteAddress { get; set; }
        public required int EntryUserId { get; set; }
        public required DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public required bool IsActive { get; set; }
    }
}
