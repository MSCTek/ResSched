using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResSched.DataModel;

namespace ResSched.Interfaces
{
    public partial interface IDataLoadService
    {
        Task<int> LoadUsers(List<User> users = null);
        Task<int> LoadResources(List<Resource> resource = null);
        Task<int> LoadResourceSchedules(List<ResourceSchedule> resourceSchedules = null);

    }
}
