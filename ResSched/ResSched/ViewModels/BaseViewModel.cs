using GalaSoft.MvvmLight;
using ResSched.Models;
using ResSched.Services;
using Xamarin.Forms;

namespace ResSched.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private bool _isBusy = false;
        private string title = string.Empty;
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        public bool IsDevEnv
        {
            get
            {
#if DEV
                return true;
#else
                return false;
#endif
            }
        }

        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }
    }
}