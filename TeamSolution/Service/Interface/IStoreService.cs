using TeamSolution.Model;

namespace TeamSolution.Service.Interface 
{
    public interface IStoreService
    {
        Task<Store> GetAllStore();
    }
}
