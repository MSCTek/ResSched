using ResSched.Interfaces;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ResSched.Services
{
    public class UploadDataService
    {
        private static UploadDataService _instance;
        private IDataService _dataService;

        private UploadDataService()
        {
            _dataService = ((App)Xamarin.Forms.Application.Current).Kernel.GetService(typeof(IDataService)) as IDataService;
        }

        public static UploadDataService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UploadDataService();
                }
                return _instance;
            }
        }

        public async Task RunQueuedUpdatesAsync(CancellationToken token)
        {
            if (_dataService == null)
            {
                _dataService = ((App)Xamarin.Forms.Application.Current).Kernel.GetService(typeof(IDataService)) as IDataService;
            }
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await _dataService.RunQueuedUpdatesAsync(token);
            }
            else
            {
                Debug.WriteLine($"No connectivity - RunQueuedUpdatesAsync cannot run");
            }
        }
    }
}