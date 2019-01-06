using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ResSched.Helpers;
using ResSched.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ResSched.Services
{
    public partial class DataService : IDataService
    {
        private static int MaxNumAttempts = 8;

        //How many are queued, failed > MaxNumAttempts times?
        public async Task<int> GetCountQueuedRecordsWAttemptsAsync()
        {
            var count = await _db.GetAsyncConnection().Table<DataModel.Queue>().Where(x => x.Success == false && x.NumAttempts > MaxNumAttempts).CountAsync();
            if (count > 0)
            {
                //sending a message to AppCenter right away with user info
                var dict = new Dictionary<string, string>
                    {
                       { "email",  App.AuthUserEmail },
                       { "userId", App.AuthUserId.ToString() }
                    };
                Analytics.TrackEvent($"ERROR: {count} Queued Records with {MaxNumAttempts} attempts", dict);
            }
            return count;
        }

        //queue a record in SQLite
        public async Task QueueAsync(Guid recordId, QueueableObjects objName)
        {
            try
            {
                DataModel.Queue queue = new DataModel.Queue()
                {
                    RecordId = recordId,
                    QueueableObject = objName.ToString(),
                    DateQueued = DateTime.UtcNow,
                    NumAttempts = 0,
                    Success = false
                };

                int count = await _db.GetAsyncConnection().InsertOrReplaceAsync(queue);

                Debug.WriteLine($"Queued {count} Queue records");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                Debug.WriteLine($"Error in {nameof(QueueAsync)}");
            }
        }

        //run the oldest 10 updates in the SQLite database that haven't had more than MaxNumAttempts retries
        public async Task RunQueuedUpdatesAsync(CancellationToken cts)
        {
            try
            {
                //Take the oldest 10 records off the queue and only take records that haven't had more than MaxNumAttempts retries
                var queue = await _db.GetAsyncConnection().Table<DataModel.Queue>().Where(x => x.Success == false && x.NumAttempts <= MaxNumAttempts).OrderBy(s => s.DateQueued).Take(10).ToListAsync();

                Debug.WriteLine($"Running {queue.Count()} Queued Updates");

                foreach (var q in queue)
                {
                    //if the system or the user has requested that the process is cancelled, then we need to stop and end gracefully.
                    if (cts.IsCancellationRequested)
                    {
                        break;
                    }

                    if (q.QueueableObject == QueueableObjects.ResourceSchedule.ToString())
                    {
                        if (await RunQueuedResourceSchedule(q))
                        {
                            q.NumAttempts += 1;
                            q.Success = true;
                            await _db.GetAsyncConnection().UpdateAsync(q);
                        }
                        else
                        {
                            q.NumAttempts += 1;
                            await _db.GetAsyncConnection().UpdateAsync(q);
                        }
                    }

                    if (q.QueueableObject == QueueableObjects.UserUpdate.ToString())
                    {
                        if (await RunQueuedUserUpdate(q))
                        {
                            q.NumAttempts += 1;
                            q.Success = true;
                            await _db.GetAsyncConnection().UpdateAsync(q);
                        }
                        else
                        {
                            q.NumAttempts += 1;
                            await _db.GetAsyncConnection().UpdateAsync(q);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public void StartSafeQueuedUpdates()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet) MessagingCenter.Send<StartUploadDataMessage>(new StartUploadDataMessage(), "StartUploadDataMessage");
        }

        private async Task<bool> RunQueuedResourceSchedule(DataModel.Queue q)
        {
            // if (_webAPIDataService == null) { return false; }

            var record = await _db.GetAsyncConnection().Table<DataModel.ResourceSchedule>().Where(x => x.ResourceScheduleId == q.RecordId).FirstOrDefaultAsync();
            if (record != null)
            {
                /*var result = await _webAPIDataService.CreateUpdateResourceScheduleAsync(record.ToDto());
                if (result.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully Sent Queued ResourceSchedule Record");
                    return true;
                }
                Analytics.TrackEvent($"Error Sending Queued ResourceSchedule record {q.RecordId}");*/
                return false;
            }
            return false;
        }

        private async Task<bool> RunQueuedUserUpdate(DataModel.Queue q)
        {
            //if (_webAPIDataService == null) { return false; }

            var record = await _db.GetAsyncConnection().Table<DataModel.User>().Where(x => x.UserId == q.RecordId).FirstOrDefaultAsync();
            if (record != null)
            {
                /*var result = await _webAPIDataService.UpdateUserAsync(record.ToDto());
                if (result.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully Sent Queued UserUpdate Record");
                    return true;
                }
                Analytics.TrackEvent($"Error Sending Queued UserUpdate record {q.RecordId}");*/
                return false;
            }
            return false;
        }
    }
}