using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ResSched.Helpers;
using ResSched.Interfaces;
using ResSched.Mappers;
using ResSched.ObjModel;
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
    public partial class DataRetrievalService : IDataRetrievalService
    {
        private static int MaxNumAttempts = 8;
        private IDatabase _db;

        public DataRetrievalService(IDatabase database)
        {
            _db = database;
        }

        public async Task<List<ObjModel.Resource>> GetAllResources()
        {
            var returnMe = new List<ObjModel.Resource>();
            var dataResults = await _db.GetAsyncConnection()
                .Table<DataModel.Resource>()
                .OrderBy(x => x.Name).ToListAsync();

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    returnMe.Add(d.ToModelObj());
                }
            }
            return returnMe;
        }

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

        public async Task<List<ObjModel.ResourceSchedule>> GetResourceSchedules(Guid resourceId, DateTime selectedDate)
        {
            var returnMe = new List<ObjModel.ResourceSchedule>();
            var dataResults = await _db.GetAsyncConnection()
                .Table<DataModel.ResourceSchedule>()
                .Where(x => x.ResourceId == resourceId && x.IsDeleted == false)
                .Where(y => y.ReservationDate == selectedDate)
                .ToListAsync();

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    returnMe.Add(d.ToModelObj());
                }
            }
            return returnMe;
        }

        public async Task<List<ObjModel.ResourceSchedule>> GetResourceSchedules(Guid resourceId)
        {
            var returnMe = new List<ObjModel.ResourceSchedule>();

            var dataResults = await _db.GetAsyncConnection()
                .Table<DataModel.ResourceSchedule>()
                .Where(x => x.ResourceId == resourceId && x.IsDeleted == false)
                .ToListAsync();

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    returnMe.Add(d.ToModelObj());
                }
            }
            return returnMe;
        }

        public async Task<List<ObjModel.ResourceSchedule>> GetResourceSchedulesForUser(string userEmail)
        {
            var returnMe = new List<ObjModel.ResourceSchedule>();

            var dataResults = await _db.GetAsyncConnection()
                .Table<DataModel.ResourceSchedule>()
                .Where(x => x.ReservedByUserEmail.ToLower() == userEmail.ToLower() && x.IsDeleted == false)
                .ToListAsync();

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    var resSchedObj = d.ToModelObj();
                    var resourceData = await _db.GetAsyncConnection()
                        .Table<DataModel.Resource>()
                        .Where(x => x.ResourceId == d.ResourceId && d.IsDeleted == false).FirstOrDefaultAsync();
                    if (resourceData != null)
                    {
                        resSchedObj.Resource = resourceData.ToModelObj();
                    }
                    returnMe.Add(resSchedObj);
                }
            }
            return returnMe;
        }

        public async Task<User> GetUserByEmail(string userEmail)
        {
            var user = await _db.GetAsyncConnection().Table<DataModel.User>().Where(x => x.Email.ToLower() == userEmail.ToLower() && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();
            return (user != null) ? user.ToModelObj() : null;
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

        public async Task<bool> SoftDeleteReservation(Guid resourceScheduleId)
        {
            var toBeDeleted = await _db.GetAsyncConnection().Table<DataModel.ResourceSchedule>().Where(x => x.ResourceScheduleId == resourceScheduleId).FirstOrDefaultAsync();
            if (toBeDeleted != null)
            {
                toBeDeleted.IsDeleted = true;
                toBeDeleted.LastModifiedBy = App.AuthUserName;
                toBeDeleted.LastModifiedDate = DateTime.Now;
                if (1 == await _db.GetAsyncConnection().UpdateAsync(toBeDeleted))
                {
                    return true;
                }
                else
                {
                    Analytics.TrackEvent("Had a problem soft deleting a resource schedule.");
                }
            }
            else
            {
                Analytics.TrackEvent("Couldn't find the corresponding record to delete!");
            }
            return false;
        }

        public void StartSafeQueuedUpdates()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet) MessagingCenter.Send<StartUploadDataMessage>(new StartUploadDataMessage(), "StartUploadDataMessage");
        }

        public async Task<int> WriteResourceSchedule(ResourceSchedule resourceSchedule)
        {
            return await _db.GetAsyncConnection().InsertOrReplaceAsync(resourceSchedule.ToModelData());
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