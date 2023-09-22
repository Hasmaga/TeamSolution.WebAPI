using TeamSolution.Model;

namespace TeamSolution.Repository.Interface 
{
    public interface IStoreRepository
    {
        Task<Store> GetAllStore();
    }
}
