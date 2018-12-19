/*
 * To add Offline Sync Support
 * 1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 * 2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see http://go.microsoft.com/fwlink/?LinkId=620342
 */
 #define OFFLINE_SYNC_ENABLED


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using ResSched.DataModel;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
#endif

namespace ResSched.Helpers
{
        public class DataManager
        {
            private static DataManager defaultInstance = null;
            private MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<Resource> resourceTable;

        const string offlineDbPath = @"localstore.db";
#else
            IMobileServiceTable<Resource> resourceTable;
#endif

            private DataManager()
            {
                client = new MobileServiceClient(Config.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);

            // Define the tables stored in the offline cache
            store.DefineTable<Resource>();

            // Initialize the sync context
            client.SyncContext.InitializeAsync(store);

            // Get a reference to the sync table
            resourceTable = client.GetSyncTable<Resource>();
#else
                // Get a reference to the online table
                todoTable = client.GetTable<TodoItem>();
#endif
            }

            public static DataManager DefaultManager
            {
                get
                {
                    if (defaultInstance == null)
                    {
                        defaultInstance = new DataManager();
                    }
                    return defaultInstance;
                }
            }

            public MobileServiceClient CurrentClient
            {
                get { return client; }
            }

            public bool IsOfflineEnabled
            {
                get { return resourceTable is IMobileServiceSyncTable<Resource>; }
            }

            public async Task<ObservableCollection<Resource>> GetTodoItemsAsync(bool syncItems = false)
            {
                try
                {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await SyncAsync();
                }
#endif

                    IEnumerable<Resource> items = await resourceTable
                        //.Where(item => !item.Done)
                        .ToEnumerableAsync();

                    return new ObservableCollection<Resource>(items);
                }
                catch (MobileServiceInvalidOperationException msioe)
                {
                    Debug.WriteLine($"Invalid sync operation: {msioe.Message}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Sync Error: {ex.Message}");
                }
                return null;
            }

            public async Task SaveTaskAsync(Resource item)
            {
                if (item.ResourceId == null)
                {
                    await resourceTable.InsertAsync(item);
                }
                else
                {
                    await resourceTable.UpdateAsync(item);
                }
            }

#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await client.SyncContext.PushAsync();
                // The first paramter is a query name, used to implement incremental sync
                await resourceTable.PullAsync("allResourceItems", resourceTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple conflict handling.  A real application would handle the various errors like network
            // conditions, server conflicts and others via the IMobileServiceSyncHandler.  This version will
            // revert to the server copy or discard the local change (i.e. server wins policy)
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        // Revert to the server copy
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard the local change
                        await error.CancelAndDiscardItemAsync();
                    }
                    Debug.WriteLine($"Error executing sync operation on table {error.TableName}: {error.Item["id"]} (Operation Discarded)");
                }
            }
        }
#endif
        }
    
}
