using GalaSoft.MvvmLight.Command;
using ResSched.Mappers;
using ResSched.Models;
using ResSched.ObjModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        //TODO: maybe move this to the config file?
        private List<int> _hours = new List<int>() { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        private Resource _resource;
        private ObservableCollection<ResourceSchedule> _schedule;
        private ObservableCollection<HourlySchedule> _scheduleByDay;
        private DateTime _selectedDate;

        public ItemDetailViewModel(Resource selected = null)
        {
            Resource = selected;
            SelectedDate = DateTime.Now.Date;
            ScheduleByDay = new ObservableCollection<HourlySchedule>();
            Schedule = new ObservableCollection<ResourceSchedule>();
        }

        public RelayCommand BookItCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                });
            }
        }

        public bool CanBook
        {
            get { return SelectedDate >= DateTime.Now.Date; }
        }

        public bool CanNavigateBackward
        {
            get { return SelectedDate.AddDays(-1) > MinDate; }
        }

        public bool CanNavigateForward
        {
            get { return SelectedDate.AddDays(1) < MaxDate; }
        }

        public DateTime MaxDate
        {
            //they can book resources 14 days in the future
            get { return DateTime.Now.AddDays(45); }
        }

        public DateTime MinDate
        {
            //they can go back and view schedules for one week in the past, but can't book anything
            get { return DateTime.Now.AddDays(-7); }
        }

        public RelayCommand NextDayCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectedDate = SelectedDate.AddDays(1);
                });
            }
        }

        public RelayCommand PreviousDayCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectedDate = SelectedDate.AddDays(-1);
                });
            }
        }

        public Resource Resource
        {
            get { return _resource; }
            set { Set(nameof(Resource), ref _resource, value); }
        }

        public ObservableCollection<ResourceSchedule> Schedule
        {
            get { return _schedule; }
            set { Set(nameof(Schedule), ref _schedule, value); }
        }

        public ObservableCollection<HourlySchedule> ScheduleByDay
        {
            get { return _scheduleByDay; }
            set { Set(nameof(ScheduleByDay), ref _scheduleByDay, value); }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (Set(nameof(SelectedDate), ref _selectedDate, value))
                {
                    //if the new value is different
                    BuildHourlySchedule();
                    RaisePropertyChanged(nameof(SelectedDateDisplay));
                    RaisePropertyChanged(nameof(SelectedDateWeekdayDisplay));
                    RaisePropertyChanged(nameof(CanNavigateForward));
                    RaisePropertyChanged(nameof(CanNavigateBackward));
                    RaisePropertyChanged(nameof(CanBook));
                }
            }
        }

        public string SelectedDateDisplay { get { return SelectedDate.ToString("dd MMM yyyy"); } }
        public string SelectedDateWeekdayDisplay { get { return SelectedDate.ToString("dddd"); } }

        public async Task Refresh()
        {
            base.Init();
            Schedule = (await base._dataService.GetResourceSchedules(Resource.ResourceId)).ToObservableCollection();
            BuildHourlySchedule();
        }

        //TODO: move this biz logic backward - out of the view model and into a helper class
        private void BuildHourlySchedule()
        {
            if (SelectedDate != DateTime.MinValue && Schedule != null)
            {
                if (ScheduleByDay == null)
                {
                    ScheduleByDay = new ObservableCollection<HourlySchedule>();
                }
                else
                {
                    ScheduleByDay.Clear();
                }

                foreach (var h in _hours)
                {
                    var hour = SelectedDate.AddHours(h);
                    var sched = Schedule
                        .Where(x => x.ReservationStartDateTime <= hour && x.ReservationEndDateTime >= hour)
                        .FirstOrDefault();

                    ScheduleByDay.Add(new HourlySchedule()
                    {
                        Hour = hour,
                        ResourceSchedule = sched
                    });
                }
            }
        }
    }
}