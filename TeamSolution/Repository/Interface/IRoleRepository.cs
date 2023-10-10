using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IRoleRepository
    {
        Task<bool> CreateRoleDAOAsync(Role role);
        Task<Guid> FindIdByRoleNameAsync(string roleName);
        Task<Role> GetRoleByIdAsync(Guid id);
        Task<List<Role>> GetAllRoleAsyncDAO();
    }
}
