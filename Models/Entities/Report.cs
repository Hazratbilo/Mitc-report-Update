using MITCRMS.Contract.Entity;
using MITCRMS.Models.Enum;

namespace MITCRMS.Models.Entities
{
    public class Report : BaseEntity
    {
        public string Tittle { get; set; }
        public string Content { get; set; }
        
        public string? FileUrl { get; set; }

        public Guid? UserID { get; set; }
        public User User { get; set; }







        public Guid? ApprovedByAdminId { get; set; }
        public DateTime? ReportDate { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public ReportStatus Status { get; set; } = ReportStatus.Pending;
    }
}
