using CodeGenHero.ResourceScheduler.Xam.ModelData.RS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResSched.Interfaces
{
    public partial interface IDataLoadService
    {
        Task<int> LoadResources(List<Resource> resource = null);

        Task<int> LoadResourceSchedules(List<ResourceSchedule> resourceSchedules = null);

        Task<int> LoadUsers(List<User> users = null);
    }
}