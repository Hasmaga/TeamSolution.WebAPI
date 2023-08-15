using TeamSolution.Model.Dto;

namespace TeamSolution.Repository.Interface
{
    public interface IRoleReposotory
    {
        Task<bool> CreateRoleAsync(NewRoleReqDto role);
    }
}
