using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using ResSched.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public enum GuestText
    {
        Sign_in_as_Guest,
        Sign_out_as_Guest,
    }

    public enum MicrosoftText
    {
        Sign_in_with_Microsoft,
        Sign_out_with_Microsoft,
    }

    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            GuestButtonText = GuestText.Sign_in_as_Guest.ToDescription();
            MicrosoftButtonText = MicrosoftText.Sign_in_with_Microsoft.ToDescription();
            ErrorDescription = string.Empty;
        }

        public RelayCommand GuestSignInCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {
                        if (GuestButtonText == GuestText.Sign_in_as_Guest.ToDescription())
                        {
                            IsUserVisible = true;
                            DisplayName = "Guest";
                            GivenName = "Guest";
                            Id = "Guest";
                            SurName = "Guest";
                            UserPrincipalName = "guest@guest.com";

                            var userId = await CheckAuthorization(UserPrincipalName);
                            if (userId != Guid.Empty)
                            {
                                RecordSuccessfulLogin(DisplayName, UserPrincipalName, userId, "Guest Login");
                            }
                            else
                            {
                                Analytics.TrackEvent($"Unknown User: {UserPrincipalName} tried to login and does not have authorization.");
                            }

                            GuestButtonText = GuestText.Sign_out_as_Guest.ToDescription();
                            MicrosoftButtonText = MicrosoftText.Sign_in_with_Microsoft.ToDescription();
                        }
                        else
                        {
                            App.AuthUserEmail = string.Empty;
                            App.AuthUserName = string.Empty;
                            IsUserVisible = false;

                            GuestButtonText = GuestText.Sign_in_as_Guest.ToDescription();
                            MicrosoftButtonText = MicrosoftText.Sign_in_with_Microsoft.ToDescription();
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

        public RelayCommand MicrosoftSignInCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    AuthenticationResult authResult = null;
                    IEnumerable<IAccount> accounts = await App.PCA.GetAccountsAsync();
                    try
                    {
                        if (MicrosoftButtonText == MicrosoftText.Sign_in_with_Microsoft.ToDescription())
                        {
                            // let's see if we have a user in our belly already
                            try
                            {
                                IAccount firstAccount = accounts.FirstOrDefault();
                                authResult = await App.PCA.AcquireTokenSilentAsync(App.AuthScopes, firstAccount);
                                await RefreshUserDataAzureADAsync(authResult.AccessToken).ConfigureAwait(false);

                                MicrosoftButtonText = MicrosoftText.Sign_out_with_Microsoft.ToDescription();
                                GuestButtonText = GuestText.Sign_in_as_Guest.ToDescription();
                            }
                            catch (MsalUiRequiredException)
                            {
                                authResult = await App.PCA.AcquireTokenAsync(App.AuthScopes, App.UiParent);
                                await RefreshUserDataAzureADAsync(authResult.AccessToken);

                                MicrosoftButtonText = MicrosoftText.Sign_out_with_Microsoft.ToDescription();
                                GuestButtonText = GuestText.Sign_in_as_Guest.ToDescription();
                            }
                        }
                        else
                        {
                            while (accounts.Any())
                            {
                                await App.PCA.RemoveAsync(accounts.FirstOrDefault());
                                accounts = await App.PCA.GetAccountsAsync();
                                App.AuthUserEmail = string.Empty;
                                App.AuthUserName = string.Empty;
                            }

                            IsUserVisible = false;

                            MicrosoftButtonText = MicrosoftText.Sign_in_with_Microsoft.ToDescription();
                            GuestButtonText = GuestText.Sign_in_as_Guest.ToDescription();
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

        private async Task<Guid> CheckAuthorization(string userEmail)
        {
            Guid userId = Guid.Empty;
            if (base.Init())
            {
                var user = await _dataService.GetUserByEmail(userEmail);
                userId = (user != null) ? user.UserId : Guid.Empty;
            }
            return userId;
        }

        private void RecordSuccessfulLogin(string userName, string userEmail, Guid userId, string loginSource)
        {
            App.AuthUserId = userId;
            App.AuthUserEmail = userEmail;
            App.AuthUserName = userName;

            Analytics.TrackEvent("Successful Login", new Dictionary<string, string>{
                            { "Source", loginSource },
                            { "UserName", userName },
                            {"UserEmail", userEmail }
                        });
            ErrorDescription = string.Empty;
        }

        private async Task RefreshUserDataAzureADAsync(string token)
        {
            //get data from API
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await client.SendAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                JObject user = JObject.Parse(responseString);
                IsUserVisible = true;
                DisplayName = user["displayName"].ToString();
                GivenName = user["givenName"].ToString();
                Id = user["id"].ToString();
                SurName = user["surname"].ToString();
                UserPrincipalName = user["userPrincipalName"].ToString();

                var userId = await CheckAuthorization(UserPrincipalName);
                if (userId != Guid.Empty)
                {
                    RecordSuccessfulLogin(DisplayName, UserPrincipalName, userId, "Microsoft");
                }
                else
                {
                    Analytics.TrackEvent($"Unknown User: {UserPrincipalName} tried to login and does not have authorization.");
                    ErrorDescription = "Sorry, we couldn't log you in. \nPlease contact the folks at Fox.Build \nto add your email address to the list. \nThanks!";
                }
            }
            else
            {
                ErrorDescription = responseString;
                Analytics.TrackEvent($"API call error: {responseString}");
            }
        }

        #region UI properties

        private string _guestButtonText;
        private bool _isUserVisible;
        private string _microsoftButtonText;

        public string GuestButtonText
        {
            get { return _guestButtonText; }
            set { Set(nameof(GuestButtonText), ref _guestButtonText, value); }
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

        #endregion UI properties

        #region display properties

        private string _displayName;
        private string _errorDescription;
        private string _givenName;
        private string _id;
        private string _providerName;
        private string _surName;
        private string _token;
        private string _userPrincipalName;

        public string DisplayName
        {
            get { return _displayName; }
            set { Set(nameof(DisplayName), ref _displayName, value); }
        }

        public string ErrorDescription
        {
            get { return _errorDescription; }
            set { Set(nameof(ErrorDescription), ref _errorDescription, value); }
        }

        public string GivenName
        {
            get { return _givenName; }
            set { Set(nameof(GivenName), ref _givenName, value); }
        }

        public string Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public string ProviderName
        {
            get { return _providerName; }
            set { Set(nameof(ProviderName), ref _providerName, value); }
        }

        public string SurName
        {
            get { return _surName; }
            set { Set(nameof(SurName), ref _surName, value); }
        }

        public string Token
        {
            get { return _token; }
            set { Set(nameof(Token), ref _token, value); }
        }

        public string UserPrincipalName

        {
            get { return _userPrincipalName; }
            set { Set(nameof(UserPrincipalName), ref _userPrincipalName, value); }
        }

        #endregion display properties
    }
}