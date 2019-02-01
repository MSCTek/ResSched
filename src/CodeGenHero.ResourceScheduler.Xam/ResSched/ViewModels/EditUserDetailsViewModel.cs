using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using Microsoft.AppCenter.Crashes;
using ResSched.Helpers;
using System;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class EditUserDetailsViewModel : BaseViewModel
    {
        private User _user;

        public EditUserDetailsViewModel(User user)
        {
            base.Init();
            User = user;
        }

        public User User
        {
            get { return _user; }
            set { Set(nameof(Resource), ref _user, value); }
        }

        internal async Task<bool> SaveAsync()
        {
            try
            {
                User.UpdatedBy = App.AuthUserName;
                User.UpdatedDate = DateTime.UtcNow;
                if (1 == await _dataService.UpdateUser(User))
                {
                    await _dataService.QueueAsync(User.Id, QueueableObjects.UserUpdate);
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