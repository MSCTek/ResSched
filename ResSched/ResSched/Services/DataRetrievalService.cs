using Microsoft.AppCenter.Analytics;
using ResSched.Interfaces;
using ResSched.Mappers;
using ResSched.ObjModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResSched.Services
{
    public partial class DataService : IDataService
    {
        private IDatabase _db;

        public DataService(IDatabase database)
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

        public async Task<int> WriteResourceSchedule(ResourceSchedule resourceSchedule)
        {
            return await _db.GetAsyncConnection().InsertOrReplaceAsync(resourceSchedule.ToModelData());
        }
    }
}