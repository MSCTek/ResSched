using CodeGenHero.DataService;
using CodeGenHero.ResourceScheduler.API.Client;
using CodeGenHero.ResourceScheduler.API.Client.Interface;
using CodeGenHero.ResourceScheduler.Service.DataService.Models;
using CodeGenHero.ResourceScheduler.Xam;
using CodeGenHero.ResourceScheduler.Xam.ModelData.RS;
using Microsoft.AppCenter.Crashes;
using ResSched.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CodeGenHero.ResourceScheduler.Service.DataService.Constants.Enums;

namespace ResSched.Services
{
    public class DataLoadService : IDataLoadService
    {
        private IDatabase _db;
        private IWebApiDataServiceRS _webAPIDataService;

        public DataLoadService(IDatabase database)
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

        public async Task<int> LoadResources()
        {
            try
            {
                DateTime? lastUpdatedDate = null;
                //if the table has records in it, drop and create a new one.
                if (await _db.GetAsyncConnection().Table<Resource>().CountAsync() > 0)
                {
                    var lastUpdated = await _db.GetAsyncConnection().Table<Resource>().OrderByDescending(x => x.UpdatedDate).FirstAsync();
                    lastUpdatedDate = lastUpdated != null ? lastUpdated?.UpdatedDate : null;
                }

                var resourcesDTO = await _webAPIDataService.GetAllPagesResourcesAsync(lastUpdatedDate);
                int count = 0;
                if (resourcesDTO.Any())
                {
                    foreach (var r in resourcesDTO)
                    {
                        count += await _db.GetAsyncConnection().InsertOrReplaceAsync(r.ToModelData());
                    }
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return 0;
            }
        }

        public async Task<int> LoadResourceSchedules()
        {
            try
            {
                DateTime? lastUpdatedDate = null;
                //if the table has records in it, drop and create a new one.
                if (await _db.GetAsyncConnection().Table<ResourceSchedule>().CountAsync() > 0)
                {
                    var lastUpdated = await _db.GetAsyncConnection().Table<ResourceSchedule>().OrderByDescending(x => x.UpdatedDate).FirstAsync();
                    lastUpdatedDate = lastUpdated != null ? lastUpdated?.UpdatedDate : null;
                }

                var resourceSchedulesDTO = await _webAPIDataService.GetAllPagesResourceSchedulesAsync(lastUpdatedDate);
                int count = 0;
                if (resourceSchedulesDTO.Any())
                {
                    foreach (var r in resourceSchedulesDTO)
                    {
                        count += await _db.GetAsyncConnection().InsertOrReplaceAsync(r.ToModelData());
                    }
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return 0;
            }
        }

        public async Task<int> LoadUsers()
        {
            try
            {
                DateTime? lastUpdatedDate = null;
                //if the table has records in it, drop and create a new one.
                if (await _db.GetAsyncConnection().Table<User>().CountAsync() > 0)
                {
                    var lastUpdated = await _db.GetAsyncConnection().Table<User>().OrderByDescending(x => x.UpdatedDate).FirstAsync();
                    lastUpdatedDate = lastUpdated != null ? lastUpdated?.UpdatedDate : null;
                }

                var usersDTO = await _webAPIDataService.GetAllPagesUsersAsync(lastUpdatedDate);
                int count = 0;
                if (usersDTO.Any())
                {
                    foreach (var u in usersDTO)
                    {
                        count += await _db.GetAsyncConnection().InsertOrReplaceAsync(u.ToModelData());
                    }
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return 0;
            }
        }
    }
}