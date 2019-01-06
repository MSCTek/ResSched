using System;
using System.Collections.Generic;
using ResSched.ViewModels;
using Xamarin.Forms;

namespace ResSched.Views
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(BindingContext != null)
            {
                var vm = (WelcomeViewModel)BindingContext;
                await vm.Init();
            }
        }
    }
}
