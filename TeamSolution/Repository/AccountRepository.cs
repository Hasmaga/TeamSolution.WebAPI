using Microsoft.EntityFrameworkCore;
using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;

namespace TeamSolution.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }       

        public async Task<bool> CreateUserAsync(Account user)
        {
            try
            {
                await _context.Accounts.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> GetUserByEmailAysnc(string email)
        {
            try
            {
                return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> GetUserByIdAsync(Guid id)
        {
            try
            {
                return await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckEmailIsExist(string email)
        {
            try            
            {
                var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
                if (user != null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(Account user)
        {
            try
            {
                _context.Accounts.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> GetUserByPhoneNumberAysnc(string phoneNumber)
        {
            try
            {
                return await _context.Accounts.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
