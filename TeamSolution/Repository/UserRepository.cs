using AutoMapper;
using TeamSolution.DAO.Interface;
using TeamSolution.Model.Dto;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDAO _userDAO;
        private readonly IRoleDAO _roleDAO;
        private readonly ILogger _logger;
        private IMapper _mapper;
        public UserRepository(IUserDAO userDAO, IRoleDAO roleDAO, ILogger<UserRepository> logger, IMapper mapper)
        {
            _userDAO = userDAO;
            _roleDAO = roleDAO;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> CreateAdminAccAsync(NewAccReqDto acc)
        {
            try
            {
                _logger.LogInformation("CreateAdminAccAsync: "+ acc.Email);
                var passwordHash = CreatePasswordHash(acc.Password, out byte[] passwordSalt);
                var newAcc = new User
                {
                    FirstName = acc.FirstName,
                    LastName = acc.LastName,
                    Email = acc.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    ForgotPasswordTimes = 0,
                    IsActive = true,
                    PhoneNumber = acc.PhoneNumber,                    
                    RoleId = await _roleDAO.FindIdByRoleNameAsync("Admin")                    
                };
                return await _userDAO.CreateUserAsync(newAcc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateMemberAccAsync(NewAccReqDto acc)
        {
            try
            {
                _logger.LogInformation("CreateMemberAccAsync: "+ acc.Email);
                var passwordHash = CreatePasswordHash(acc.Password, out byte[] passwordSalt);
                var newAcc = new User
                {
                    FirstName = acc.FirstName,
                    LastName = acc.LastName,
                    Email = acc.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    ForgotPasswordTimes = 0,
                    IsActive = true,
                    PhoneNumber = acc.PhoneNumber,
                    RoleId = await _roleDAO.FindIdByRoleNameAsync("Member")
                };
                return await _userDAO.CreateUserAsync(newAcc);
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        #region private
        private string CreatePasswordHash(string password, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                return Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
        #endregion        
    }
}
