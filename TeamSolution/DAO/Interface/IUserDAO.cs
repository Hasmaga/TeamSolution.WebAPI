using TeamSolution.Model;

namespace TeamSolution.DAO.Interface
{
    public interface IUserDAO
    {        
        Task<bool> CreateUserAsync(User user);
        Task<bool> GetUserIsExistAsync(string email, string passwordHash);        
    }
}
