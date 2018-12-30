using ResSched.Interfaces;
using ResSched.Mappers;
using ResSched.ObjModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResSched.Services
{
    public class DataRetrievalService : IDataRetrievalService
    {
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

        public async Task<List<ObjModel.ResourceSchedule>> GetResourceSchedules(Guid resourceId, DateTime selectedDate)
        {
            var returnMe = new List<ObjModel.ResourceSchedule>();
            var dataResults = await _db.GetAsyncConnection()
                .Table<DataModel.ResourceSchedule>()
                .Where(x => x.ResourceId == resourceId)
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
                .Where(x => x.ResourceId == resourceId)
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
                .Where(x => x.ReservedByUserEmail.ToLower() == userEmail.ToLower())
                .ToListAsync();

            if (dataResults.Any())
            {
                foreach (var d in dataResults)
                {
                    var resSchedObj = d.ToModelObj();
                    var resourceData = await _db.GetAsyncConnection()
                        .Table<DataModel.Resource>()
                        .Where(x => x.ResourceId == d.ResourceId).FirstOrDefaultAsync();
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
            var user = await _db.GetAsyncConnection().Table<DataModel.User>().Where(x => x.Email.ToLower() == userEmail.ToLower()).FirstOrDefaultAsync();
            return (user != null) ? user.ToModelObj() : null;
        }

        public async Task<int> WriteResourceSchedule(ResourceSchedule resourceSchedule)
        {
            return await _db.GetAsyncConnection().InsertOrReplaceAsync(resourceSchedule.ToModelData());
        }
    }
}