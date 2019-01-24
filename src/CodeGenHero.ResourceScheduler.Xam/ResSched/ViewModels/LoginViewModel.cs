using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ResSched.Helpers;
using ResSched.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ResSched.ViewModels
{
    public enum MicrosoftText
    {
        Sign_in_with_Microsoft,
        Sign_out_with_Microsoft,
    }

    public class LoginViewModel : BaseViewModel
    {
        private const string _signIn = "Sign In";
        private const string _signInAsGuest = "Sign In as Guest";
        private const string _signOut = "Sign Out";
        private const string _signOutAsGuest = "Sign Out as Guest";
        private string _guestButtonText;
        private bool _isUserVisible;
        private string _microsoftButtonText;
        private string _userButtonText;
        private string _userDisplayName;
        private string _userEnteredEmail;
        private string _userPrincipalName;

        public LoginViewModel()
        {
            GuestButtonText = _signInAsGuest;
            UserButtonText = _signIn;
            MicrosoftButtonText = MicrosoftText.Sign_in_with_Microsoft.ToDescription();
            ErrorDescription = string.Empty;
        }

        public bool CanUserLogin
        {
            get { return string.IsNullOrEmpty(UserEnteredEmail) ? false : true; }
        }

        public RelayCommand GoToResourcesCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    MainPage rootPage = Application.Current.MainPage as MainPage;
                    await rootPage.NavigateFromMenu(0);
                });
            }
        }

        public string GuestButtonText
        {
            get { return _guestButtonText; }
            set { Set(nameof(GuestButtonText), ref _guestButtonText, value); }
        }

        public RelayCommand GuestSignInCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {
                        if (GuestButtonText == _signInAsGuest)
                        {
                            UserPrincipalName = "guest@guest.com";
                            UserDisplayName = "Guest";

                            var user = await CheckAuthorization(UserPrincipalName);
                            if (user != null)
                            {
                                IsUserVisible = true;
                                RecordSuccessfulLogin(user, "Guest Login");
                                GuestButtonText = _signOutAsGuest;
                                UserButtonText = _signIn;
                            }
                            else
                            {
                                Logout();
                                ErrorDescription = $"Can't log you in! Please contact someone at Fox.Build.";
                                Analytics.TrackEvent($"Unknown User: {UserPrincipalName} tried to login and does not have authorization.");
                            }
                        }
                        else
                        {
                            Logout();
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorDescription = ex.Message;
                        Crashes.TrackError(ex);
                    }
                });
            }
        }

        public bool IsUserVisible
        {
            get { return _isUserVisible; }
            set { Set(nameof(IsUserVisible), ref _isUserVisible, value); }
        }

        public string MicrosoftButtonText
        {
            get { return _microsoftButtonText; }
            set { Set(nameof(MicrosoftButtonText), ref _microsoftButtonText, value); }
        }

        public string UserButtonText
        {
            get { return _userButtonText; }
            set { Set(nameof(UserButtonText), ref _userButtonText, value); }
        }

        public string UserDisplayName

        {
            get { return _userDisplayName; }
            set { Set(nameof(UserDisplayName), ref _userDisplayName, value); }
        }

        public string UserEnteredEmail
        {
            get { return _userEnteredEmail; }
            set
            {
                if (Set(nameof(UserEnteredEmail), ref _userEnteredEmail, value))
                {
                    RaisePropertyChanged(nameof(CanUserLogin));
                }
            }
        }

        public string UserPrincipalName

        {
            get { return _userPrincipalName; }
            set { Set(nameof(UserPrincipalName), ref _userPrincipalName, value); }
        }

        public RelayCommand UserSignInCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {
                        if (UserButtonText == _signIn)
                        {
                            UserPrincipalName = UserEnteredEmail;
                            UserDisplayName = UserEnteredEmail;

                            var user = await CheckAuthorization(UserPrincipalName);
                            if (user != null)
                            {
                                IsUserVisible = true;
                                RecordSuccessfulLogin(user, "User Login");
                                GuestButtonText = _signInAsGuest;
                                UserButtonText = _signOut;
                            }
                            else
                            {
                                Logout();
                                ErrorDescription = $"Can't log you in! Please contact someone at Fox.Build.";
                                Analytics.TrackEvent($"Unknown User: {UserPrincipalName} tried to login and does not have authorization.");
                            }
                        }
                        else
                        {
                            Logout();
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorDescription = ex.Message;
                        Crashes.TrackError(ex);
                    }
                });
            }
        }

        internal async Task InitVM()
        {
            GuestButtonText = _signInAsGuest;
            UserButtonText = _signIn;
            ErrorDescription = string.Empty;

            string userInBelly = Preferences.Get(Config.Preference_Email, null);
            if (!string.IsNullOrEmpty(userInBelly))
            {
                var user = await CheckAuthorization(userInBelly);
                if (user != null)
                {
                    IsUserVisible = true;
                    UserPrincipalName = userInBelly;
                    UserDisplayName = userInBelly;
                    UserEnteredEmail = userInBelly;
                    RecordSuccessfulPassiveLogin(user);
                    GuestButtonText = _signInAsGuest;
                    UserButtonText = _signOut;
                }
                else
                {
                    Analytics.TrackEvent($"Unknown User: {userInBelly} is a past authorized user, attempted login and was rejected.");
                }
            }
        }

        private async Task<User> CheckAuthorization(string userEmail)
        {
            Guid userId = Guid.Empty;
            if (base.Init())
            {
                var user = await _dataService.GetUserByEmail(userEmail);
                return (user != null) ? user : null;
            }
            return null;
        }

        private void Logout()
        {
            App.AuthUserEmail = string.Empty;
            App.AuthUserName = string.Empty;
            IsUserVisible = false;
            Preferences.Clear();

            GuestButtonText = _signInAsGuest;
            UserButtonText = _signIn;
        }

        private async void RecordSuccessfulLogin(User user, string loginSource)
        {
            App.AuthUserId = user.Id;
            App.AuthUserEmail = user.Email;
            App.AuthUserName = user.Name;
            if (user.Email.ToLower() != "guest@guest.com")
            {
                Preferences.Set(Config.Preference_Email, user.Email);
            }

            Analytics.TrackEvent("Successful Login", new Dictionary<string, string>{
                            { "Source", loginSource },
                            { "UserName", user.Name },
                            { "UserEmail", user.Email },
                            { "UserId", user.Id.ToString() }
                        });
            ErrorDescription = string.Empty;

            user.LastLoginDate = DateTime.UtcNow;
            user.UpdatedBy = user.UserName;
            user.UpdatedDate = DateTime.UtcNow;
            user.InstallationId = (await AppCenter.GetInstallIdAsync()).ToString();

            if (1 == await _dataService.UpdateUser(user))
            {
                await _dataService.QueueAsync(user.Id, QueueableObjects.UserUpdate);
                _dataService.StartSafeQueuedUpdates();
            }
        }

        private async void RecordSuccessfulPassiveLogin(User user)
        {
            App.AuthUserId = user.Id;
            App.AuthUserEmail = user.Email;
            App.AuthUserName = user.Name;

            Analytics.TrackEvent("Successful Login", new Dictionary<string, string>{
                            { "Source", "PassiveLogin" },
                            { "UserName", user.Name },
                            { "UserEmail", user.Email },
                            { "UserId", user.Id.ToString() }
                        });
            ErrorDescription = string.Empty;

            user.LastLoginDate = DateTime.UtcNow;
            user.UpdatedBy = user.UserName;
            user.UpdatedDate = DateTime.UtcNow;
            user.InstallationId = (await AppCenter.GetInstallIdAsync()).ToString();

            if (1 == await _dataService.UpdateUser(user))
            {
                await _dataService.QueueAsync(user.Id, QueueableObjects.UserUpdate);
                _dataService.StartSafeQueuedUpdates();
            }
        }
    }
}