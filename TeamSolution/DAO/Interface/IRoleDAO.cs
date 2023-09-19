using TeamSolution.Model;

namespace TeamSolution.DAO.Interface
{
    public interface IRoleDAO
    {
        Task<bool> CreateRoleDAOAsync(Role role);
        Task<Guid> FindIdByRoleNameAsync(string roleName);
        Task<Role> GetRoleByIdAsync(Guid id);
        Task<List<Role>> GetAllRoleAsyncDAO();
    }
}
