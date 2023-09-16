using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamSolution.DAO.Interface;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Model.Dto;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class RoleRepository : IRoleReposotory
    {
        private readonly IRoleDAO _roleDAO;
        private readonly IUserDAO _userDAO;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _configuration;
        private IMapper _mapper;
        public RoleRepository(IRoleDAO roleDAO, IUserDAO userDAO, ILogger<RoleRepository> logger, IMapper mapper, IHttpContextAccessor http, IConfiguration configuration)
        {
            _roleDAO = roleDAO;
            _userDAO = userDAO;
            _logger = logger;
            _mapper = mapper;
            _http = http;
            _configuration = configuration;
        }

        public async Task<bool> CreateRoleAsync(NewRoleReqDto role)
        {
            try
            {
                _logger.LogInformation("CreateRoleAsync: "+ role.RoleName);
                var userLogged = await _userDAO.GetUserByIdAsync(GetSidLogged());
                var Adminrole = await _roleDAO.FindIdByRoleNameAsync("Admin");
                if (CheckTokenIsExpires(_http.HttpContext.Request.Headers["Authorization"].ToString()) == true
                    && userLogged.RoleId != Adminrole == false)
                {
                    throw new Exception(ErrorCode.NOT_AUTHORIZED);
                }
                if (await _roleDAO.FindIdByRoleNameAsync(role.RoleName) != Guid.Empty)
                {
                    throw new Exception(ErrorCode.ROLE_IS_EXIST);
                }
                return await _roleDAO.CreateRoleDAOAsync(_mapper.Map<Role>(role));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods  
        private Guid GetSidLogged()
        {
            var result = Guid.Parse(_http.HttpContext.User.FindFirstValue(ClaimTypes.Sid));
            return result;
        }
        private bool CheckTokenIsExpires(string token)
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
        #endregion
    }
}
