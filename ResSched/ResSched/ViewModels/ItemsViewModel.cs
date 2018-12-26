using ResSched.Mappers;
using ResSched.ObjModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ObservableCollection<Resource> _items;

        public ItemsViewModel()
        {
            Title = "Browse Resources";
            Items = new ObservableCollection<Resource>();
        }

        public ObservableCollection<Resource> Items
        {
            get { return _items; }
            set { Set(nameof(Items), ref _items, value); }
        }

        public async Task Init()
        {
            if (base.Init())
            {
                Items = (await _dataService.GetAllResources()).ToObservableCollection();
            }
        }
    }
}