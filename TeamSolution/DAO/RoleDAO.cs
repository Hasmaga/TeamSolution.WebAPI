using Microsoft.EntityFrameworkCore;
using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;

namespace TeamSolution.DAO
{
    public class RoleDAO : IRoleDAO
    {
        private readonly ApplicationDbContext _context;
        public RoleDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRoleDAOAsync(Role role)
        {
            try
            {
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> FindIdByRoleNameAsync(string roleName)
        {
            try
            {
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == roleName);
                if (role != null)
                {
                    return role.Id;
                }
                return Guid.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Role> GetRoleByIdAsync(Guid id)
        {
            try
            {
                return await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
