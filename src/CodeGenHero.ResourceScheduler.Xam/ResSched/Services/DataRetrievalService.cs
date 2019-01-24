using CodeGenHero.DataService;
using CodeGenHero.ResourceScheduler.API.Client;
using CodeGenHero.ResourceScheduler.API.Client.Interface;
using CodeGenHero.ResourceScheduler.Service.DataService.Models;
using CodeGenHero.ResourceScheduler.Xam;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ResSched.Helpers;
using ResSched.Interfaces;
using ResSched.Mappers;
using ResSched.ModelData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static CodeGenHero.ResourceScheduler.Service.DataService.Constants.Enums;
using dataModel = CodeGenHero.ResourceScheduler.Xam.ModelData.RS;
using objModel = CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;

namespace ResSched.Services
{
    public partial class DataRetrievalService : IDataRetrievalService
    {
        private static int MaxNumAttempts = 8;
        private IDatabase _db;
        private IWebApiDataServiceRS _webAPIDataService;

        public DataRetrievalService(IDatabase database)
        {
            _db = database;
            var webApiExecutionContextType = new RSWebApiExecutionContextType();
            webApiExecutionContextType.Current = (int)ExecutionContextTypes.Base;

            WebApiExecutionContext context = new WebApiExecutionContext(
                executionContextType: webApiExecutionContextType,
                baseWebApiUrl: Config.BaseWebApiUrl,
                baseFileUrl: string.Empty,
                connectionIdentifier: null);

            _webAPIDataService = new WebApiDataServiceRS(null, context);
        }

        public async Task<List<objModel.Resource>> GetAllResources()
        {
            var returnMe = new List<objModel.Resource>();
            var dataResults = await _db.GetAsyncConnection()
                .Table<dataModel.Resource>()
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
            var count = await _db.GetAsyncConnection().Table<Queue>().Where(x => x.Success == false && x.NumAttempts > MaxNumAttempts).CountAsync();
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

        public async Task<List<objModel.ResourceSchedule>> GetResourceSchedules(Guid resourceId, DateTime selectedDate)
        {
            var returnMe = new List<objModel.ResourceSchedule>();
            var dataResults = await _db.GetAsyncConnection()
                .Table<dataModel.ResourceSchedule>()
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

        public async Task<List<objModel.ResourceSchedule>> GetResourceSchedules(Guid resourceId)
        {
            var returnMe = new List<objModel.ResourceSchedule>();

            var dataResults = await _db.GetAsyncConnection()
                .Table<dataModel.ResourceSchedule>()
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

        public async Task<List<objModel.ResourceSchedule>> GetResourceSchedulesForUser(Guid userId)
        {
            var returnMe = new List<objModel.ResourceSchedule>();

            var dataResults = await _db.GetAsyncConnection()
                .Table<dataModel.ResourceSchedule>()
                .Where(x => x.ReservedByUserId == userId && x.IsDeleted == false && x.ReservationDate >= DateTime.Today)
                .ToListAsync();

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    var resSchedObj = d.ToModelObj();
                    var resourceData = await _db.GetAsyncConnection()
                        .Table<dataModel.Resource>()
                        .Where(x => x.Id == d.ResourceId && d.IsDeleted == false).FirstOrDefaultAsync();
                    if (resourceData != null)
                    {
                        resSchedObj.Resource = resourceData.ToModelObj();
                    }
                    returnMe.Add(resSchedObj);
                }
            }
            return returnMe;
        }

        public async Task<objModel.User> GetUserByEmail(string userEmail)
        {
            var user = await _db.GetAsyncConnection().Table<dataModel.User>().Where(x => x.Email.ToLower() == userEmail.ToLower() && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();
            return (user != null) ? user.ToModelObj() : null;
        }

        //queue a record in SQLite
        public async Task QueueAsync(Guid recordId, QueueableObjects objName)
        {
            try
            {
                ModelData.Queue queue = new ModelData.Queue()
                {
                    RecordId = recordId,
                    QueueableObject = objName.ToString(),
                    DateQueued = DateTime.UtcNow,
                    NumAttempts = 0,
                    Success = false
                };

                int count = await _db.GetAsyncConnection().InsertOrReplaceAsync(queue);

                Debug.WriteLine($"Queued {recordId} of type {objName}");
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
                var queue = await _db.GetAsyncConnection().Table<ModelData.Queue>().Where(x => x.Success == false && x.NumAttempts <= MaxNumAttempts).OrderBy(s => s.DateQueued).Take(10).ToListAsync();

                Debug.WriteLine($"Running {queue.Count()} Queued Updates");

                foreach (var q in queue)
                {
                    //if the system or the user has requested that the process is cancelled, then we need to stop and end gracefully.
                    if (cts.IsCancellationRequested)
                    {
                        break;
                    }

                    if (q.QueueableObject == QueueableObjects.ResourceScheduleCreate.ToString())
                    {
                        if (await RunQueuedResourceScheduleCreate(q))
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
                    else if (q.QueueableObject == QueueableObjects.ResourceScheduleUpdate.ToString())
                    {
                        if (await RunQueuedResourceScheduleUpdate(q))
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
                    else if (q.QueueableObject == QueueableObjects.UserUpdate.ToString())
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
            var toBeDeleted = await _db.GetAsyncConnection().Table<dataModel.ResourceSchedule>().Where(x => x.Id == resourceScheduleId).FirstOrDefaultAsync();
            if (toBeDeleted != null)
            {
                toBeDeleted.IsDeleted = true;
                toBeDeleted.UpdatedBy = App.AuthUserName;
                toBeDeleted.UpdatedDate = DateTime.Now;
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

        public async Task<int> UpdateUser(objModel.User user)
        {
            return await _db.GetAsyncConnection().UpdateAsync(user.ToModelData());
        }

        public async Task<int> WriteResourceSchedule(objModel.ResourceSchedule resourceSchedule)
        {
            //write this to the PendingResourceSchedule table
            var pending = new dataModel.PendingResourceSchedule()
            {
                CreatedBy = resourceSchedule.CreatedBy,
                CreatedDate = resourceSchedule.CreatedDate,
                Id = resourceSchedule.Id,
                IsDeleted = resourceSchedule.IsDeleted,
                ReservationDate = resourceSchedule.ReservationDate,
                ReservationEndDateTime = resourceSchedule.ReservationEndDateTime,
                ReservationNotes = resourceSchedule.ReservationNotes,
                ReservationStartDateTime = resourceSchedule.ReservationStartDateTime,
                ReservedByUserId = resourceSchedule.ReservedByUserId,
                ReservedForUser = resourceSchedule.ReservedForUser,
                ReservedOnDateTime = resourceSchedule.ReservedOnDateTime,
                ResourceId = resourceSchedule.ResourceId,
                UpdatedBy = resourceSchedule.UpdatedBy,
                UpdatedDate = resourceSchedule.UpdatedDate
            };

            return await _db.GetAsyncConnection().InsertOrReplaceAsync(pending);
        }

        private async Task<bool> RunQueuedResourceScheduleCreate(ModelData.Queue q)
        {
            if (_webAPIDataService == null) { return false; }

            var record = await _db.GetAsyncConnection().Table<dataModel.PendingResourceSchedule>().Where(x => x.Id == q.RecordId).FirstOrDefaultAsync();
            if (record != null)
            {
                var result = await _webAPIDataService.CreateResourceScheduleAsync(record.ToDto());
                if (result.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully Sent Queued PendingResourceSchedule Create Record");
                    return true;
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    //do something here with the conflict
                }
                Analytics.TrackEvent($"Error Sending Queued PendingResourceSchedule Create record {q.RecordId}");
                return false;
            }
            return false;
        }

        private async Task<bool> RunQueuedResourceScheduleUpdate(ModelData.Queue q)
        {
            if (_webAPIDataService == null) { return false; }

            var record = await _db.GetAsyncConnection().Table<dataModel.PendingResourceSchedule>().Where(x => x.Id == q.RecordId).FirstOrDefaultAsync();
            if (record != null)
            {
                var result = await _webAPIDataService.UpdateResourceScheduleAsync(record.ToDto());
                if (result.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully Sent Queued PendingResourceSchedule Update Record");
                    return true;
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    //do something here with the conflict
                }
                Analytics.TrackEvent($"Error Sending Queued PendingResourceSchedule Update record {q.RecordId}");
                return false;
            }
            return false;
        }

        private async Task<bool> RunQueuedUserUpdate(ModelData.Queue q)
        {
            if (_webAPIDataService == null) { return false; }

            var record = await _db.GetAsyncConnection().Table<dataModel.User>().Where(x => x.Id == q.RecordId).FirstOrDefaultAsync();
            if (record != null)
            {
                var result = await _webAPIDataService.UpdateUserAsync(record.ToDto());
                if (result.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully Sent Queued UserUpdate Record");
                    return true;
                }
                Analytics.TrackEvent($"Error Sending Queued UserUpdate record {q.RecordId}");
                return false;
            }
            return false;
        }
    }
}