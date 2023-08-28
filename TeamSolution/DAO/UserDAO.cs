using Microsoft.EntityFrameworkCore;
using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;

namespace TeamSolution.DAO
{
    public class UserDAO : IUserDAO
    {
        private readonly ApplicationDbContext _context;
        public UserDAO(ApplicationDbContext context)
        {
            _context = context;
        }       

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByEmailAysnc(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
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
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
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
    }
}
