using Microsoft.EntityFrameworkCore;
using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;

namespace TeamSolution.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public RoleRepository(ApplicationDbContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateRoleDAOAsync(Role role)
        {
            try
            {
                _logger.LogInformation("CreateRoleDAOAsync: " + role.RoleName);
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                _logger.LogError("Error create role: " + role.RoleName);
                throw;
            }
        }

        public async Task<Guid> FindIdByRoleNameAsync(string roleName)
        {
            try
            {
                _logger.LogInformation("FindIdByRoleNameAsync: " + roleName);
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == roleName);
                if (role != null)
                {
                    return role.Id;
                }
                return Guid.Empty;
            }
            catch (Exception)
            {
                _logger.LogInformation("Error find role: " + roleName);
                throw;
            }
        }

        // Get all role in database
        public async Task<List<Role>> GetAllRoleAsyncDAO()
        {
            try
            {
                _logger.LogInformation("GetAllRoleAsyncDAO");
                return await _context.Roles.ToListAsync();
            }
            catch (Exception)
            {
                _logger.LogError("Error get all role");
                throw;
            }
        }

        public async Task<Role> GetRoleByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("GetRoleByIdAsync: " + id);
                return await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                _logger.LogError("Error get role by id: " + id);
                throw;
            }
        }
    }
}
