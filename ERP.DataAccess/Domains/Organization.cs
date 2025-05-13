using System.ComponentModel.DataAnnotations;

namespace ERP.DataAccess.Domains
{
    public class Organization
    {
        [Key]
        public int ID { get; set; }

        public string OrganizatioName { get; set; }
        public string OwnerName { get; set; }
        public string OrganizationAddress { get; set; }
        public string MobileNo { get; set; }
        public string TinNumber { get; set; }
        public string? WebsiteAddress { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}