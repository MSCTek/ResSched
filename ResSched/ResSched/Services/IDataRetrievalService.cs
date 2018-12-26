using ResSched.ObjModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResSched.Services
{
    public partial interface IDataRetrievalService
    {
        Task<List<ResourceSchedule>> GetResourceSchedulesForUser(string userEmail);
        Task<List<Resource>> GetAllResources();
        Task<List<ResourceSchedule>> GetResourceSchedules(Guid resourceId);
    }
}