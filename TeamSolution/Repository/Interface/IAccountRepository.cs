using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IAccountRepository
    {        
        Task<bool> CreateUserAsync(Account user);
        Task<Account> GetUserByEmailAysnc(string email);
        Task<Account> GetUserByPhoneNumberAysnc(string email);
        Task<Account> GetUserByIdAsync(Guid id);
        Task<bool> CheckEmailIsExist(string email);
        Task<bool> UpdateUserAsync(Account user);
    }
}
