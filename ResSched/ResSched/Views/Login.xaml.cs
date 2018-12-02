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
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void OnSignInSignOutMicrosoft(object sender, EventArgs e)
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await App.PCA.GetAccountsAsync();
            try
            {
                if (btnSignInSignOutMicrosoft.Text == "Sign in with Microsoft")
                {
                    // let's see if we have a user in our belly already
                    try
                    {
                        IAccount firstAccount = accounts.FirstOrDefault();
                        authResult = await App.PCA.AcquireTokenSilentAsync(App.AuthScopes, firstAccount);
                        await RefreshUserDataAsync(authResult.AccessToken).ConfigureAwait(false);
                        Device.BeginInvokeOnMainThread(() => {
                            btnSignInSignOutMicrosoft.Text = "Sign out with Microsoft";
                            btnSignInSignOutGuest.Text = "Sign in as Guest";
                        });
                    }
                    catch (MsalUiRequiredException ex)
                    {
                        authResult = await App.PCA.AcquireTokenAsync(App.AuthScopes, App.UiParent);
                        await RefreshUserDataAsync(authResult.AccessToken);
                        Device.BeginInvokeOnMainThread(() => {
                            btnSignInSignOutMicrosoft.Text = "Sign out with Microsoft";
                            btnSignInSignOutGuest.Text = "Sign in as Guest";
                        });
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

                    slUser.IsVisible = false;
                    Device.BeginInvokeOnMainThread(() => {
                        btnSignInSignOutMicrosoft.Text = "Sign in with Microsoft";
                        btnSignInSignOutGuest.Text = "Sign in as Guest";
                    });
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
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

                slUser.IsVisible = true;

                Device.BeginInvokeOnMainThread(() =>
                {
                    lblDisplayName.Text = user["displayName"].ToString();
                    lblGivenName.Text = user["givenName"].ToString();
                    lblId.Text = user["id"].ToString();
                    lblSurname.Text = user["surname"].ToString();
                    lblUserPrincipalName.Text = user["userPrincipalName"].ToString();
                });
            }
            else
            {
                await DisplayAlert("Something went wrong with the API call", responseString, "Dismiss");
            }
        }

        private void RefreshGuestDataAsync()
        {
            slUser.IsVisible = true;

            Device.BeginInvokeOnMainThread(() =>
            {
                lblDisplayName.Text = "Guest";
                lblGivenName.Text = "Guest";
                lblId.Text = "Guest";
                lblSurname.Text = "Guest";
                lblUserPrincipalName.Text = "Guest";
            });
        }

        private async void OnSignInSignOutGuest(object sender, EventArgs e)
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await App.PCA.GetAccountsAsync();
            try
            {
                if (btnSignInSignOutGuest.Text == "Sign in as Guest")
                {
                    App.AuthUserEmail = "guest@guest.com";
                    App.AuthUserName = "Guest";
                    RefreshGuestDataAsync();
                    Device.BeginInvokeOnMainThread(() => {
                        btnSignInSignOutGuest.Text = "Sign out as Guest";
                        btnSignInSignOutMicrosoft.Text = "Sign in with Microsoft";
                    });
                }
                else
                {
                    App.AuthUserEmail = string.Empty;
                    App.AuthUserName = string.Empty;
                    slUser.IsVisible = false;
                    Device.BeginInvokeOnMainThread(() => {
                        btnSignInSignOutGuest.Text = "Sign in as Guest"; 
                        btnSignInSignOutMicrosoft.Text = "Sign in with Microsoft";
                    });
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}