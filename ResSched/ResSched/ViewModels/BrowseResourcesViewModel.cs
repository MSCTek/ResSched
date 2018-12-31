using ResSched.Mappers;
using ResSched.ObjModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class BrowseResourcesViewModel : BaseViewModel
    {
        private ObservableCollection<Resource> _items;

        public BrowseResourcesViewModel()
        {
            Title = "Browse Resources";
            Items = new ObservableCollection<Resource>();
        }

        public ObservableCollection<Resource> Items
        {
            get { return _items; }
            set { Set(nameof(Items), ref _items, value); }
        }

        public async Task InitVM()
        {
            if (base.Init())
            {
                Items = (await _dataService.GetAllResources()).ToObservableCollection();
            }
        }
    }
}