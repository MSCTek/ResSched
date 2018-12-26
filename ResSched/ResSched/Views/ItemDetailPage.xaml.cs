using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ResSched.Models;
using ResSched.ViewModels;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if(viewModel != null)
            {
                await viewModel.Refresh();
            }
        }
    }
}