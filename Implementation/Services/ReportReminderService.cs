using MITCRMS.Interface.Repository;
using MITCRMS.Interface.Services;
using MITCRMS.Models.DTOs;
using MITCRMS.Models.Enum;

namespace MITCRMS.Implementation.Services
{
    public class ReportReminderService(IReportRepository reportRepository) : IReportReminderService
    {
        private readonly IReportRepository _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        public async Task<BaseResponse> ProcessRemindersAsync(ReminderLevel level)
        {
            var defaulters = await _reportRepository.GetStaffWithoutReportWeek();

            if (defaulters is null || !defaulters.Any())
            {
                return new BaseResponse
                {
                    Message = "No report defaulters",
                    Status = false
                };
            }     

            foreach(var staff in defaulters)
            {
                var (subject, body) = BuildMessage(level);
            }
        }

        private (string Subject, string Body) BuildMessage(ReminderLevel level)
        {
            return level switch
            {
                ReminderLevel.Friendly =>
                ("Weekly Report Reminder",
                "Kindly submit your weekly report before close of Business"),

                ReminderLevel.FollowUp =>
                ("Weekly Report Follow-Up",
                "This is a follow-up reminder to submit your weekly report. Please ensure it is submitted before the end of the day."),

                ReminderLevel.FinalNotice =>
                ("Final Notice - Weekly Report",
                "Final reminder: Please submit your weekly report immediately"),



                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
