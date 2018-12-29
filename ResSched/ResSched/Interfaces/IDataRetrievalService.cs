using ResSched.ObjModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResSched.Interfaces
{
    public partial interface IDataRetrievalService
    {
        Task<List<Resource>> GetAllResources();

        Task<List<ResourceSchedule>> GetResourceSchedules(Guid resourceId);

        Task<List<ResourceSchedule>> GetResourceSchedules(Guid resourceId, DateTime selectedDate);

        Task<List<ResourceSchedule>> GetResourceSchedulesForUser(string userEmail);

        Task<int> WriteResourceSchedule(ResourceSchedule resourceSchedule);
    }
}