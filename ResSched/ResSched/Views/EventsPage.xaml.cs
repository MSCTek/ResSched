using ResSched.Models.MeetupEvents;
using ResSched.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        private EventsViewModel viewModel;

        public EventsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new EventsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var vm = BindingContext as EventsViewModel;
                await vm.InitVM();
            }
        }

        private void LearnMoreButton_Clicked(object sender, System.EventArgs e)
        {
            var result = ((Button)sender).BindingContext as Result;
            Device.OpenUri(new Uri(result.event_url));
        }
    }
}