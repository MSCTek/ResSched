using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ResSched.Models;
using ResSched.Views;
using ResSched.Services;
using Ninject;
using ResSched.Mappers;
using ResSched.ObjModel;

namespace ResSched.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private IDataRetrievalService _dataService;
        private ObservableCollection<Resource> _items;
        //public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse Resources";
            Items = new ObservableCollection<Resource>();

            var ker = ((ResSched.App)Xamarin.Forms.Application.Current).Kernel;
            PreInit(ker.Get<IDataRetrievalService>());

        }
        private void PreInit(IDataRetrievalService dataRetrievalService)
        {

            _dataService = dataRetrievalService;
        }
        public async Task Refresh()
        {
           Items = (await _dataService.GetAllResources()).ToObservableCollection();
        }
        public ObservableCollection<Resource> Items
        {
            get { return _items; }
            set { Set(nameof(Items), ref _items, value); }
        }

    }
}