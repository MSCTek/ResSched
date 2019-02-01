using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;

namespace ResSched.ViewModels
{
    public class EditResourceDetailsViewModel : BaseViewModel
    {
        private Resource _resource;

        public EditResourceDetailsViewModel(Resource resource)
        {
            Resource = resource;
        }

        public Resource Resource
        {
            get { return _resource; }
            set { Set(nameof(Resource), ref _resource, value); }
        }
    }
}