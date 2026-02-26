using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MITCRMS.Interface.Services;
using MITCRMS.Models.Enum;

namespace Mitc_report_Update.BackgroundWorker
{
    public class WeeklyReportReminderBackgroundService(IServiceProvider serviceProvider, ILogger<WeeklyReportReminderBackgroundService> logger) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        private readonly ILogger<WeeklyReportReminderBackgroundService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var reminderLevel = GetReminderLevel();

                    if (reminderLevel.HasValue)
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var service = scope.ServiceProvider
                            .GetRequiredService<IReportReminderService>();

                        await service.ProcessRemindersAsync(reminderLevel.Value);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error running weekly reminder job.");
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private ReminderLevel? GetReminderLevel()
        {
            var now = DateTime.Now;

            return now.DayOfWeek switch
            {
                DayOfWeek.Friday when now.Hour == 17
                    => ReminderLevel.Friendly,

                DayOfWeek.Saturday when now.Hour == 10
                    => ReminderLevel.FollowUp,

                DayOfWeek.Sunday when now.Hour == 16
                    => ReminderLevel.FinalNotice,

                _ => null
            };
        }
    }
}