﻿using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.OAuth;

namespace ResSched.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _guestButtonText;
        private bool _isUserVisible;
        private string _microsoftButtonText;
        private bool _showSlackSignIn;

        public LoginViewModel()
        {
            GuestButtonText = "Sign in as Guest";
            MicrosoftButtonText = "Sign in with Microsoft";

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    ShowSlackSignIn = false;
                    break;
                default:
                    ShowSlackSignIn = true;
                    break;
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
                return new RelayCommand(() =>
                {
                    try
                    {
                        if (GuestButtonText == "Sign in as Guest")
                        {
                            App.AuthUserEmail = "guest@guest.com";
                            App.AuthUserName = "Guest";
                            IsUserVisible = true;
                            DisplayName = "Guest";
                            GivenName = "Guest";
                            Id = "Guest";
                            SurName = "Guest";
                            UserPrincipalName = "Guest";

                            GuestButtonText = "Sign out as Guest";
                            MicrosoftButtonText = "Sign in with Microsoft";
                        }
                        else
                        {
                            App.AuthUserEmail = string.Empty;
                            App.AuthUserName = string.Empty;
                            IsUserVisible = false;

                            GuestButtonText = "Sign in as Guest";
                            MicrosoftButtonText = "Sign in with Microsoft";
                        }
                    }
                    catch (Exception ex)
                    {
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
                        if (MicrosoftButtonText == "Sign in with Microsoft")
                        {
                            // let's see if we have a user in our belly already
                            try
                            {
                                IAccount firstAccount = accounts.FirstOrDefault();
                                authResult = await App.PCA.AcquireTokenSilentAsync(App.AuthScopes, firstAccount);
                                await RefreshUserDataAsync(authResult.AccessToken).ConfigureAwait(false);

                                MicrosoftButtonText = "Sign out with Microsoft";
                                GuestButtonText = "Sign in as Guest";
                            }
                            catch (MsalUiRequiredException ex)
                            {
                                authResult = await App.PCA.AcquireTokenAsync(App.AuthScopes, App.UiParent);
                                await RefreshUserDataAsync(authResult.AccessToken);

                                MicrosoftButtonText = "Sign out with Microsoft";
                                GuestButtonText = "Sign in as Guest";
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

                            MicrosoftButtonText = "Sign in with Microsoft";
                            GuestButtonText = "Sign in as Guest";
                        }
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                });
            }
        }

        public bool ShowSlackSignIn
        {
            get { return _showSlackSignIn; }
            set { Set(nameof(ShowSlackSignIn), ref _showSlackSignIn, value); }
        }

        public RelayCommand SlackSignInCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var authenticationResult = await OAuthAuthenticator.Authenticate();

                    if (authenticationResult)
                    {
                        ProviderName = authenticationResult.Account.ProviderName;
                        Id = authenticationResult.Account.Id;
                        DisplayName = authenticationResult.Account.DisplayName;
                        Token = authenticationResult.Account.AccessToken.RefreshToken;
                    }
                    else
                    {
                        ErrorDescription = authenticationResult.ErrorDescription;
                    }
                });
            }
        }

        public async Task RefreshUserDataAsync(string token)
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
            }
            else
            {
                Analytics.TrackEvent($"API call error: {responseString}");
            }
        }
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