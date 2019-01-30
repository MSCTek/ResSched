using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using ResSched.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ResSched.Interfaces
{
    public partial interface IDataRetrievalService
    {
        Task<List<Resource>> GetAllResources();

        Task<List<User>> GetAllUsers();

        Task<List<ResourceSchedule>> GetResourceSchedules(Guid resourceId);

        Task<List<ResourceSchedule>> GetResourceSchedules(Guid resourceId, DateTime selectedDate);

        Task<List<ResourceSchedule>> GetResourceSchedulesForUser(Guid userId);

        Task<User> GetUserByEmail(string userEmail);

        Task QueueAsync(Guid recordId, QueueableObjects obj);

        Task RunQueuedUpdatesAsync(CancellationToken token);

        Task<bool> SoftDeleteReservation(Guid reservationId);

        void StartSafeQueuedUpdates();

        Task<int> UpdateUser(User user);

        Task<int> WriteResourceSchedule(ResourceSchedule resourceSchedule);
    }
}