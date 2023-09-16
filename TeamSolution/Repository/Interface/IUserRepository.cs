using TeamSolution.Model.Dto;

namespace TeamSolution.Repository.Interface
{
    public interface IUserRepository
    {
        Task<bool> CreateAdminAccAsync(NewAccReqDto acc);
        Task<bool> CreateMemberAccAsync(NewAccReqDto acc);
        Task<string> LoginAsync(LoginReqDto loginReqDto);
        
    }
}
