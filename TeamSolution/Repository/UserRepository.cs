using AutoMapper;
using TeamSolution.DAO.Interface;
using TeamSolution.Model.Dto;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using TeamSolution.Enum;
using System.Net.Mail;

namespace TeamSolution.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDAO _userDAO;
        private readonly IRoleDAO _roleDAO;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _configuration;
        public UserRepository(IUserDAO userDAO, IRoleDAO roleDAO, ILogger<UserRepository> logger, IHttpContextAccessor http, IConfiguration configuration)
        {
            _userDAO = userDAO;
            _roleDAO = roleDAO;
            _logger = logger;
            _http = http;
            _configuration = configuration;
        }

        public async Task<bool> CreateAdminAccAsync(NewAccReqDto acc)
        {
            try
            {
                _logger.LogInformation("CreateAdminAccAsync: "+ acc.Email);
                // Check conditions
                var userLogged = await _userDAO.GetUserByIdAsync(GetSidLogged());
                var Adminrole = await _roleDAO.FindIdByRoleNameAsync("Admin");
                if (
                    _http.HttpContext != null 
                    && CheckTokenIsExpires(_http.HttpContext.Request.Headers["Authorization"].ToString()) == true                                    
                    && userLogged.RoleId != Adminrole == false)
                {
                    throw new Exception(ErrorCode.NOT_AUTHORIZED);
                }                      
                if (!await _userDAO.CheckEmailIsExist(acc.Email))
                {
                    throw new Exception(ErrorCode.USER_IS_EXIST);
                }
                // Create new account
                var passwordHash = CreatePasswordHash(acc.Password, out byte[] passwordSalt);
                var newAcc = new User
                (
                    acc.FirstName,
                    acc.LastName,
                    acc.Email,
                    passwordHash,
                    Convert.ToBase64String(passwordSalt),
                    acc.PhoneNumber,
                    true,
                    0,
                    await _roleDAO.FindIdByRoleNameAsync("Admin"),
                    await _roleDAO.FindIdByRoleNameAsync("Active"),
                    acc.Address,
                    0,
                    false,
                    DateTime.Now,
                    DateTime.Now,
                    Guid.Empty
                );
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
                if (!await _userDAO.CheckEmailIsExist(acc.Email))
                {
                    throw new Exception(ErrorCode.USER_IS_EXIST);
                }
                var passwordHash = CreatePasswordHash(acc.Password, out byte[] passwordSalt);
                var newAcc = new User
                (
                    acc.FirstName,
                    acc.LastName,
                    acc.Email,
                    passwordHash,
                    Convert.ToBase64String(passwordSalt),
                    acc.PhoneNumber,
                    true,
                    0,
                    await _roleDAO.FindIdByRoleNameAsync("User"),
                    await _roleDAO.FindIdByRoleNameAsync("Active"),
                    acc.Address,
                    0,
                    false,
                    DateTime.Now,
                    DateTime.Now,
                    Guid.Empty
                );
                return await _userDAO.CreateUserAsync(newAcc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> LoginAsync(LoginReqDto login)
        {
            try
            {
                _logger.LogInformation("LoginAsync: "+ login.Email);
                var user = await _userDAO.GetUserByEmailAysnc(login.Email);
                if (user == null || !VerifyPasswordHash(login.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt))) 
                    throw new Exception(ErrorCode.USER_NOT_FOUND);                
                return CreateBearerToken(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private string CreatePasswordHash(string password, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                return Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        private Guid GetSidLogged()
        {
            if(_http.HttpContext == null)
            {
                throw new Exception(ErrorCode.NOT_AUTHORIZED);

            }
            var result = Guid.Parse(_http.HttpContext.User.FindFirstValue(ClaimTypes.Sid));
            return result;
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
        
        private string CreateBearerToken(User logUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, logUser.Id.ToString())                
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(                                            
                                             claims: claims,
                                             expires: DateTime.Now.AddDays(7),
                                             signingCredentials: creds
                                            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        // Check the token is valid and not expired
        private bool CheckTokenIsExpires (string token)
        {
            // remove bearer from token
            token = token.Substring(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),                
                ValidateIssuer = false,
                ValidateAudience = false                
            }, out SecurityToken validatedToken);
            return validatedToken.ValidTo < DateTime.UtcNow;
        }   
        
        // Send email to user
        private bool SendEmail(string email, string subject, string body)
        {
            string mailFrom = _configuration.GetSection("Email:EmailFrom").Value;
            MailMessage mess = new MailMessage();
            return true;
        }
        #endregion        
    }
}
