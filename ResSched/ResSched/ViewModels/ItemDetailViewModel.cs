using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ResSched.Mappers;
using ResSched.Models;
using ResSched.ObjModel;
using ResSched.Services;

namespace ResSched.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private Resource _resource;
        private ObservableCollection<ResourceSchedule> _schedule;
        private ObservableCollection<HourlySchedule> _scheduleByDay;
        private DateTime _selectedDay;

        public ItemDetailViewModel(Resource selected = null)
        {
            Resource = selected;
            SelectedDay = DateTime.Now.Date.AddDays(4);
            ScheduleByDay = new ObservableCollection<HourlySchedule>();
            Schedule = new ObservableCollection<ResourceSchedule>();
        }

        public ObservableCollection<HourlySchedule> ScheduleByDay
        {
            get { return _scheduleByDay; }
            set { Set(nameof(ScheduleByDay), ref _scheduleByDay, value); }
        }

        public ObservableCollection<ResourceSchedule> Schedule
        {
            get { return _schedule; }
            set { Set(nameof(Schedule), ref _schedule, value); }
        }

        public DateTime SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                if (Set(nameof(SelectedDay), ref _selectedDay, value))
                {
                    //if the new value is different
                    BuildHourlySchedule();
                }
            }
        }

        private void BuildHourlySchedule()
        {
            if (SelectedDay != DateTime.MinValue && Schedule != null)
            {
                if (ScheduleByDay == null)
                {
                    ScheduleByDay = new ObservableCollection<HourlySchedule>();
                }
                else
                {
                    ScheduleByDay.Clear();
                }
                List<int> hours = new List<int>() { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                foreach (var h in hours)
                {
                    var hour = SelectedDay.AddHours(h);
                    ScheduleByDay.Add(new HourlySchedule()
                    {
                        Hour = hour,
                        ResourceSchedule = Schedule.FirstOrDefault(x => x.ReservationDateTime == hour)
                    });

                }

            }

        }

        public Resource Resource
        {
            get { return _resource; }
            set { Set(nameof(Resource), ref _resource, value); }
        }

        public async Task Refresh()
        {
            base.Init();
            Schedule = (await base._dataService.GetResourceSchedules(Resource.ResourceId)).ToObservableCollection();
            BuildHourlySchedule();
        }


    }

    public class HourlySchedule : ObservableObject
    {
        private DateTime _hour;
        private ResourceSchedule _resourceSchedule;

        public string ReservedMessage { get { return ResourceSchedule == null ? "Open" : ResourceSchedule.ReservedForUser; } }

        public string HourDisplay { get { return this.Hour.ToLongTimeString(); } }

        public DateTime Hour
        {
            get { return _hour; }
            set { Set(nameof(Hour), ref _hour, value); }
        }

        public ResourceSchedule ResourceSchedule
        {
            get { return _resourceSchedule; }
            set { Set(nameof(ResourceSchedule), ref _resourceSchedule, value); }
        }

        public RelayCommand ScheduleResourceCommand
        {
            get
            {
                return new RelayCommand(() =>
                {

                });
            }
        }
    }
}
