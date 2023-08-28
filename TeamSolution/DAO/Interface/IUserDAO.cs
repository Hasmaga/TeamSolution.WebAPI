using TeamSolution.Model;

namespace TeamSolution.DAO.Interface
{
    public interface IUserDAO
    {        
        Task<bool> CreateUserAsync(User user);
        Task<User> GetUserByEmailAysnc(string email);   
        Task<User> GetUserByIdAsync(Guid id);
        Task<bool> CheckEmailIsExist(string email);
    }
}
