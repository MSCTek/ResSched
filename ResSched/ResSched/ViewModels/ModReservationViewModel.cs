using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Analytics;
using ResSched.Helpers;
using ResSched.Models;
using ResSched.ObjModel;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace ResSched.ViewModels
{
    public class ModReservationViewModel : BaseViewModel
    {
        private ObservableCollection<string> _endHourList;
        private ObservableCollection<HourlySchedule> _hourlySchedules;
        private ResourceSchedule _resourceSchedule;
        private DateTime _selectedDate;
        private string _selectedEndHour;
        private string _selectedStartHour;
        private ObservableCollection<string> _startHourList;

        //constructor for existing records
        public ModReservationViewModel(HourlySchedule hourlySchedule)
        {
            base.Init();
            ResourceSchedule = hourlySchedule.ResourceSchedule;
            SelectedStartHour = ResourceSchedule.ReservationStartDateTime.ToShortTimeString();
            SelectedEndHour = ResourceSchedule.ReservationEndDateTime.ToShortTimeString();
            SelectedDate = hourlySchedule.ResourceSchedule.ReservationStartDateTime.Date;
            BuildHourlySchedule();
        }

        //constructor for new records
        public ModReservationViewModel(DateTime selectedDate, Resource resource)
        {
            base.Init();
            ResourceSchedule = new ResourceSchedule()
            {
                Resource = resource,
                ResourceId = resource.ResourceId
            };
            SelectedDate = selectedDate;
            BuildHourlySchedule();
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }

        public ObservableCollection<string> EndHourList
        {
            get { return _endHourList; }
            set { Set(nameof(EndHourList), ref _endHourList, value); }
        }

        public ObservableCollection<HourlySchedule> HourlySchedules
        {
            get { return _hourlySchedules; }
            set { Set(nameof(HourlySchedules), ref _hourlySchedules, value); }
        }

        public RelayCommand ReserveCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    if (Validate())
                    {
                        if (string.IsNullOrEmpty(ResourceSchedule.CreatedBy))
                        {
                            ResourceSchedule.CreatedBy = App.AuthUserName;
                            ResourceSchedule.CreatedDate = DateTime.Now;
                        }
                        ResourceSchedule.LastModifiedBy = App.AuthUserName;
                        ResourceSchedule.LastModifiedDate = DateTime.Now;
                        ResourceSchedule.ReservationDate = SelectedDate;

                        if (ResourceSchedule.ResourceScheduleId == Guid.Empty || ResourceSchedule.ResourceScheduleId == null)
                        {
                            ResourceSchedule.ResourceScheduleId = Guid.NewGuid();
                        }

                        if (string.IsNullOrEmpty(ResourceSchedule.ReservedByUserEmail))
                        {
                            ResourceSchedule.ReservedByUserEmail = App.AuthUserEmail;
                        }

                        if (ResourceSchedule.ReservedByUserId == Guid.Empty)
                        {
                            ResourceSchedule.ReservedByUserId = App.AuthUserId;
                        }

                        ResourceSchedule.ReservedOnDateTime = DateTime.Now;

                        //update the SQLite db
                        if (1 == await _dataService.WriteResourceSchedule(ResourceSchedule))
                        {
                            //TODO: queue the background process to upload
                            //navigate back
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                        else
                        {
                            //error
                            Analytics.TrackEvent("Can't write Resource Records to the db!");
                        }
                    }
                });
            }
        }

        public ResourceSchedule ResourceSchedule
        {
            get { return _resourceSchedule; }
            set { Set(nameof(ResourceSchedule), ref _resourceSchedule, value); }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { Set(nameof(SelectedDate), ref _selectedDate, value); }
        }

        public string SelectedDateDisplay { get { return SelectedDate.ToLongDateString(); } }

        public string SelectedEndHour
        {
            get { return _selectedEndHour; }
            set
            {
                if (Set(nameof(SelectedEndHour), ref _selectedEndHour, value))
                {
                    if (SelectedDate != DateTime.MinValue && !string.IsNullOrEmpty(SelectedEndHour))
                    {
                        ResourceSchedule.ReservationEndDateTime = ParseSelected(SelectedDate, SelectedEndHour);
                    }
                }
            }
        }

        public string SelectedStartHour
        {
            get { return _selectedStartHour; }
            set
            {
                if (Set(nameof(SelectedStartHour), ref _selectedStartHour, value))
                {
                    if (SelectedDate != DateTime.MinValue && !string.IsNullOrEmpty(SelectedStartHour))
                    {
                        ResourceSchedule.ReservationStartDateTime = ParseSelected(SelectedDate, SelectedStartHour);
                    }
                }
            }
        }

        public ObservableCollection<string> StartHourList
        {
            get { return _startHourList; }
            set { Set(nameof(StartHourList), ref _startHourList, value); }
        }

        private async void BuildHourlySchedule()
        {
            HourlySchedules = await _dataService.BuildHourlyScheduleAsync(SelectedDate, ResourceSchedule.ResourceId);

            if (HourlySchedules.Any())
            {
                //if the page has new values, make sure they are marked in the list:
                if (SelectedStartHour != null)
                {
                    var start = HourlySchedules.Where(x => x.HourDisplay == SelectedStartHour).FirstOrDefault();
                    if (start != null && !start.IsReserved)
                    {
                        //stop here
                        start.IsInReservationProcess = true;
                    }
                }

                //build up the lists for the dropdowns
                var startHourList = new ObservableCollection<string>();
                var endHourList = new ObservableCollection<string>();

                foreach (var h in HourlySchedules)
                {
                    if (HourlySchedules.IndexOf(h) != 0)
                    {
                        endHourList.Add(h.HourDisplay);
                    }
                    if (HourlySchedules.IndexOf(h) != Config.Hours.Count - 1)
                    {
                        startHourList.Add(h.HourDisplay);
                    }
                }

                StartHourList = startHourList;
                EndHourList = endHourList;

                RaisePropertyChanged(nameof(SelectedStartHour));
                RaisePropertyChanged(nameof(SelectedEndHour));
            }
        }

        private DateTime ParseSelected(DateTime selectedDate, string selectedHour)
        {
            const string FMT = "yyyy-MM-dd-h:mm tt";
            string newDateStr = $"{selectedDate.Year}-{selectedDate.Month}-{selectedDate.Day}-{selectedHour}";
            return DateTime.ParseExact(newDateStr, FMT, CultureInfo.InvariantCulture);
        }

        private bool Validate()
        {
            bool allGood = true;
            //fields need to be filled in and the end date/time needs to be after the before date/time

            //check that we have a reserved for field
            if (string.IsNullOrEmpty(ResourceSchedule.ReservedForUser))
            {
                allGood = false;
                Application.Current.MainPage.DisplayAlert("Error", "Please note who/what the reservation is for!", "OK");
                return allGood;
            }

            if (string.IsNullOrEmpty(SelectedStartHour) || string.IsNullOrEmpty(SelectedEndHour))
            {
                allGood = false;
                Application.Current.MainPage.DisplayAlert("Error", "Please select a start and end time for your reservation.", "OK");
                return allGood;
            }

            if (ResourceSchedule.ReservationStartDateTime == DateTime.MinValue || ResourceSchedule.ReservationEndDateTime == DateTime.MinValue)
            {
                allGood = false;
                Application.Current.MainPage.DisplayAlert("Error", "We are having trouble making your reservation. Please let an admin know.", "OK");
                return allGood;
            }

            //reservations should start before they end
            if (ResourceSchedule.ReservationStartDateTime >= ResourceSchedule.ReservationEndDateTime)
            {
                allGood = false;
                Application.Current.MainPage.DisplayAlert("Error", "Your reservation should begin before it ends!", "OK");
                return allGood;
            }

            //lastly, make sure there is no conflict with existing reservations
            foreach (var h in HourlySchedules)
            {
                if (h.IsReserved && h.ResourceSchedule != null)
                {
                    //if the new/edited reservation is inbetween and of the existing reservations...
                    if (ResourceSchedule.ReservationStartDateTime <= h.Hour && ResourceSchedule.ReservationEndDateTime >= h.Hour)
                    {
                        //check if we are currently editing this record.
                        if (ResourceSchedule.ResourceScheduleId != h.ResourceSchedule.ResourceScheduleId)
                        {
                            allGood = false;
                            Application.Current.MainPage.DisplayAlert("Error", "Your reservation conflicts with an existing reservation.", "OK");
                            return allGood;
                        }
                    }
                }
            }
            return allGood;
        }
    }
}