using TeamSolution.Model.Dto;

namespace TeamSolution.Repository.Interface
{
    public interface IUserRepository
    {
        Task<bool> CreateAdminAccAsync(NewAccReqDto acc);
    }
}
