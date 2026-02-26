namespace MITCRMS.Models.DTOs.Report
{
    public class CreateReportRequestModel
    {
     
            public Guid DepartmentId { get; set; } 
            public string Tittle { get; set; }     
            public string Content { get; set; }
            public string? FileUrl { get; set; } = null;

    }
}
