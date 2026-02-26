namespace MITCRMS.Implementation.Messaging.Models
{
    public class SendReportReminder : Base
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string ActionText { get; set; } = "Report Report";
    }
}
