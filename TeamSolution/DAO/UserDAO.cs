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

        public async Task<bool> GetUserIsExistAsync(string email, string passwordHash)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == passwordHash);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
