using Microsoft.AppCenter.Crashes;
using ResSched.DataModel;
using ResSched.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResSched.Services
{
    public class DataLoadSampleDataService : IDataLoadService
    {
        private IDatabase _db;

        public DataLoadSampleDataService(IDatabase database)
        {
            _db = database;
        }

        public async Task<int> LoadResources(List<Resource> resources = null)
        {
            try
            {
                //if the table has records in it, drop and create a new one.
                if (await _db.GetAsyncConnection().Table<Resource>().CountAsync() > 0)
                {
                    await _db.GetAsyncConnection().DropTableAsync<Resource>();
                    await Task.Delay(500);
                    await _db.GetAsyncConnection().CreateTableAsync<Resource>();
                    await Task.Delay(500);
                }

                if (resources == null)
                {
                    resources = new List<Resource>()
                    {
                        SampleData.SampleResource.MeetingRoom1,
                        SampleData.SampleResource.MeetingRoom2,
                        SampleData.SampleResource.MeetingRoom3,
                        SampleData.SampleResource.XCarve
                    };
                }

                return await _db.GetAsyncConnection().InsertAllAsync(resources);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return 0;
            }
        }

        public async Task<int> LoadResourceSchedules(List<ResourceSchedule> resourceSchedules = null)
        {
            try
            {
                //if the table has records in it, drop and create a new one.
                if (await _db.GetAsyncConnection().Table<ResourceSchedule>().CountAsync() > 0)
                {
                    await _db.GetAsyncConnection().DropTableAsync<ResourceSchedule>();
                    await Task.Delay(500);
                    await _db.GetAsyncConnection().CreateTableAsync<ResourceSchedule>();
                    await Task.Delay(500);
                }

                if (resourceSchedules == null)
                {
                    resourceSchedules = new List<ResourceSchedule>()
                    {
                        SampleData.SampleResourceSchedule.SampleSchedule1,
                        SampleData.SampleResourceSchedule.SampleSchedule2,
                    };
                }

                return await _db.GetAsyncConnection().InsertAllAsync(resourceSchedules);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return 0;
            }
        }

        public async Task<int> LoadUsers(List<User> users = null)
        {
            try
            {
                //if the table has records in it, drop and create a new one.
                if (await _db.GetAsyncConnection().Table<User>().CountAsync() > 0)
                {
                    await _db.GetAsyncConnection().DropTableAsync<User>();
                    await Task.Delay(500);
                    await _db.GetAsyncConnection().CreateTableAsync<User>();
                    await Task.Delay(500);
                }

                if (users == null)
                {
                    users = new List<User>()
                    {
                        SampleData.SampleUser.SampleUserGeorge,
                        SampleData.SampleUser.SampleUserMicky,
                        SampleData.SampleUser.SampleUserMinnie,
                        SampleData.SampleUser.SampleUserGuest
                    };
                }

                return await _db.GetAsyncConnection().InsertAllAsync(users);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return 0;
            }
        }
    }
}