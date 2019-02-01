using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using Microsoft.AppCenter.Crashes;
using ResSched.Helpers;
using System;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class EditResourceDetailsViewModel : BaseViewModel
    {
        private Resource _resource;

        public EditResourceDetailsViewModel(Resource resource)
        {
            base.Init();
            Resource = resource;
        }

        public Resource Resource
        {
            get { return _resource; }
            set { Set(nameof(Resource), ref _resource, value); }
        }

        internal async Task<bool> SaveAsync()
        {
            try
            {
                Resource.UpdatedBy = App.AuthUserName;
                Resource.UpdatedDate = DateTime.UtcNow;
                if (1 == await _dataService.UpdateResource(Resource))
                {
                    await _dataService.QueueAsync(Resource.Id, QueueableObjects.ResourceUpdate);
                    _dataService.StartSafeQueuedUpdates();
                }
                return true;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return false;
            }
        }
    }
}