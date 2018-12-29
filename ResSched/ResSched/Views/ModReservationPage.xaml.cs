using ResSched.Models;
using ResSched.ObjModel;
using ResSched.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModReservationPage : ContentPage
    {
        private ModReservationViewModel viewModel;

        //constructor for creating new resource schedules
        public ModReservationPage(DateTime selectedDate, Resource resource)
        {
            InitializeComponent();
            BindingContext = viewModel = new ModReservationViewModel(selectedDate, resource);
        }

        //constructor for editing existing resource schedules
        public ModReservationPage(HourlySchedule hourlySchedule)
        {
            InitializeComponent();
            BindingContext = viewModel = new ModReservationViewModel(hourlySchedule);
        }

        private void EditReservation_Tapped(object sender, EventArgs e)
        {
        }
    }
}