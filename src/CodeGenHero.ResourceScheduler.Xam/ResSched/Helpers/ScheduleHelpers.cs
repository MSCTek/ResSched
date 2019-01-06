using ResSched.Interfaces;
using ResSched.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ResSched.Helpers
{
    public static class ScheduleHelpers
    {
        public static async Task<ObservableCollection<HourlySchedule>> BuildHourlyScheduleAsync(this IDataRetrievalService _dataRetrievalService, DateTime selectedDate, Guid resourceId)
        {
            var hourlySchedule = new ObservableCollection<HourlySchedule>();
            var schedules = await _dataRetrievalService.GetResourceSchedules(resourceId, selectedDate);
            foreach (var h in Config.Hours)
            {
                var hour = selectedDate.AddHours(h);
                var sched = schedules.Where(x => x.ReservationStartDateTime <= hour && x.ReservationEndDateTime >= hour).FirstOrDefault();

                hourlySchedule.Add(new HourlySchedule()
                {
                    Hour = hour,
                    ResourceSchedule = sched
                });
            }
            return hourlySchedule;
        }
    }
}