using TeamSolution.Model;

namespace TeamSolution.DAO.Interface
{
    public interface IRoleDAO
    {
        Task<bool> CreateRoleDAOAsync(Role role);
        Task<Guid> FindIdByRoleNameAsync(string roleName);
    }
}
