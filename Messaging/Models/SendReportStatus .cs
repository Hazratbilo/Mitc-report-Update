namespace MITCRMS.Implementation.Messaging.Models
{
    public class SendReportStatus : Base
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string ActionText { get; set; } = "Open Report Portal";

        // ✅ ADD THESE FOR STATUS EMAIL
        public string Status { get; set; }          // Approved / Rejected
        public string ActionLink { get; set; }      // Link to report
    }
}