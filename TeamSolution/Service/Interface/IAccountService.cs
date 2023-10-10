using TeamSolution.Model.Dto;

namespace TeamSolution.Service.Interface
{
    public interface IAccountService
    {
        Task<bool> CreateAdminAccAsync(NewAccReqDto acc);
        Task<bool> CreateMemberAccAsync(NewAccReqDto acc);
        Task<string> LoginAsync(LoginReqDto loginReqDto);
        
    }
}
