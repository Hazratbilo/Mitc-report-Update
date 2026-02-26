
using MITCRMS.Models.DTOs.Department;
using MITCRMS.Models.DTOs.Users;
using MITCRMS.Models.Enum;


namespace MITCRMS.Models.DTOs.Report
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid? UserId { get; set; }
        public UserDto User { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }

        public string Tittle { get; set; }
        public string Content { get; set; }
        public string? FileUrl { get; set; }
        public ReportStatus Status { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public Guid? ApprovedByAdminId { get; set; }

    }

}
