using TeamSolution.Model;
using TeamSolution.Model.Dto;

namespace TeamSolution.Service.Interface
{
    public interface IRoleService
    {
        Task<bool> CreateRoleAsync(NewRoleReqDto role);
        Task<List<Role>> GetAllRoleAsync();
    }
}
