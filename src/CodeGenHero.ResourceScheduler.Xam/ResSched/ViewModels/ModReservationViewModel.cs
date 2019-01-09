using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Analytics;
using ResSched.Helpers;
using ResSched.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace ResSched.ViewModels
{
    public class ModReservationViewModel : BaseViewModel
    {
        private ObservableCollection<string> _endHourList;
        private ObservableCollection<HourlySchedule> _hourlySchedules;
        private string _pageHeaderText;
        private ResourceSchedule _resourceSchedule;
        private DateTime _selectedDate;
        private string _selectedEndHour;
        private string _selectedStartHour;
        private ObservableCollection<string> _startHourList;
        private string _submitButtonText;

        //constructor for existing records
        public ModReservationViewModel(HourlySchedule hourlySchedule)
        {
            base.Init();
            SubmitButtonText = "Update Reservation";
            PageHeaderText = "Modify an Existing Reservation";
            ResourceSchedule = hourlySchedule.ResourceSchedule;
            SelectedDate = hourlySchedule.ResourceSchedule.ReservationStartDateTime.Date;
            BuildHourlyScheduleAsync();
        }

        //constructor for new records
        public ModReservationViewModel(DateTime selectedDate, Resource resource)
        {
            base.Init();
            SubmitButtonText = "Reserve";
            PageHeaderText = "Make a Reservation";
            ResourceSchedule = new ResourceSchedule()
            {
                Resource = resource,
                ResourceId = resource.Id
            };
            SelectedDate = selectedDate;
            BuildHourlyScheduleAsync();
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

        public string PageHeaderText
        {
            get { return _pageHeaderText; }
            set { Set(nameof(PageHeaderText), ref _pageHeaderText, value); }
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
                            ResourceSchedule.CreatedDate = DateTime.UtcNow;
                        }
                        ResourceSchedule.UpdatedBy = App.AuthUserName;
                        ResourceSchedule.UpdatedDate = DateTime.UtcNow;
                        ResourceSchedule.ReservationDate = SelectedDate;

                        bool isNew = false;
                        if (ResourceSchedule.Id == Guid.Empty || ResourceSchedule.Id == null)
                        {
                            ResourceSchedule.Id = Guid.NewGuid();
                            isNew = true;
                        }

                        if (ResourceSchedule.ReservedByUserId == Guid.Empty || ResourceSchedule.ReservedByUserId == null)
                        {
                            ResourceSchedule.ReservedByUserId = App.AuthUserId;
                        }

                        ResourceSchedule.ReservedOnDateTime = DateTime.Now;

                        //update the SQLite db
                        if (1 == await _dataService.WriteResourceSchedule(ResourceSchedule))
                        {
                            //if SQLite updated successfully, update the queue
                            if (isNew)
                            {
                                await _dataService.QueueAsync(ResourceSchedule.Id, QueueableObjects.ResourceScheduleCreate);
                            }
                            else
                            {
                                await _dataService.QueueAsync(ResourceSchedule.Id, QueueableObjects.ResourceScheduleUpdate);
                            }
                            _dataService.StartSafeQueuedUpdates();

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

        public string SelectedDateDisplay { get { return SelectedDate.ToString("dddd, dd MMM yyyy"); } }

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
                    if (SelectedDate != DateTime.MinValue && !string.IsNullOrEmpty(SelectedStartHour) && StartHourList != null)
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

        public string SubmitButtonText
        {
            get { return _submitButtonText; }
            set { Set(nameof(SubmitButtonText), ref _submitButtonText, value); }
        }

        private async void BuildHourlyScheduleAsync()
        {
            HourlySchedules = await _dataService.BuildHourlyScheduleAsync(SelectedDate, ResourceSchedule.ResourceId);

            if (HourlySchedules.Any())
            {
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

                SelectedStartHour = ResourceSchedule.ReservationStartDateTime.ToShortTimeString();
                SelectedEndHour = ResourceSchedule.ReservationEndDateTime.ToShortTimeString();
            }
        }

        private DateTime ParseSelected(DateTime selectedDate, string selectedHour)
        {
            string newDateStr = $"{selectedDate.Month}/{selectedDate.Day}/{selectedDate.Year} {selectedHour}";
            Debug.WriteLine($"CHECK THIS DATE: {newDateStr}");

            DateTime dt;
            var converted = DateTime.TryParse(newDateStr, out dt);
            if (converted)
            {
                // Converted okay.
                //var newFormat = dt.ToString("yyyy/MM/dd hh:mm:ss");
                // Outputs: 2001/01/01 01:00:00
                return dt;
            }
            else
            {
                // Failed to convert.
                //try something else
                const string FMT = "MM/dd/yyyy hh:mm tt";
                return DateTime.ParseExact(newDateStr, FMT, CultureInfo.InvariantCulture);
            }
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
                        if (ResourceSchedule.Id != h.ResourceSchedule.Id)
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