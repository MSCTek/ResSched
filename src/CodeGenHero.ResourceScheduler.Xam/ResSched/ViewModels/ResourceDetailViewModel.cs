using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using GalaSoft.MvvmLight.Command;
using ResSched.Helpers;
using ResSched.Mappers;
using ResSched.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class ResourceDetailViewModel : BaseViewModel
    {
        private ObservableCollection<HourlySchedule> _hourlySchedules;
        private Resource _resource;
        private ObservableCollection<ResourceSchedule> _resourceSchedules;
        private DateTime _selectedDate;

        public ResourceDetailViewModel(Resource selected = null)
        {
            HourlySchedules = new ObservableCollection<HourlySchedule>();
            ResourceSchedules = new ObservableCollection<ResourceSchedule>();
            Resource = selected;
            SelectedDate = DateTime.Now.Date;
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

        public ObservableCollection<HourlySchedule> HourlySchedules
        {
            get { return _hourlySchedules; }
            set { Set(nameof(HourlySchedules), ref _hourlySchedules, value); }
        }

        public DateTime MaxDate
        {
            //they can book resources 45 days in the future
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

        public ObservableCollection<ResourceSchedule> ResourceSchedules
        {
            get { return _resourceSchedules; }
            set { Set(nameof(ResourceSchedules), ref _resourceSchedules, value); }
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
                    //RaisePropertyChanged(nameof(SelectedDateDisplay));
                    //RaisePropertyChanged(nameof(SelectedDateWeekdayDisplay));
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
            ResourceSchedules = (await base._dataService.GetResourceSchedules(Resource.Id)).ToObservableCollection();
            HourlySchedules = await _dataService.BuildHourlyScheduleAsync(SelectedDate, Resource.Id);
        }

        private async void BuildHourlySchedule()
        {
            if (base.Init())
            {
                HourlySchedules = await _dataService.BuildHourlyScheduleAsync(SelectedDate, Resource.Id);
            }
        }
    }
}