using System.Threading.Tasks;

namespace ResSched.Interfaces
{
    public partial interface IDataLoadService
    {
        Task<bool> HeartbeatCheck();

        Task<int> LoadResources();

        Task<int> LoadResourceSchedules();

        Task<int> LoadUsers();
    }
}