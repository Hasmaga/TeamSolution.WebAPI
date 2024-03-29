﻿using TeamSolution.Service.Interface;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using TeamSolution.Enum;
using System.Net.Mail;
using TeamSolution.Repository;
using TeamSolution.ViewModel.Account;

namespace TeamSolution.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AccountService(
            IAccountRepository accountRepository, 
            IRoleRepository roleRepository, 
            ILogger<AccountService> logger, 
            IHttpContextAccessor http, 
            IConfiguration configuration,
            IStatusRepository statusRepository,
            IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _logger = logger;
            _http = http;
            _configuration = configuration;
            _statusRepository = statusRepository;
            _emailService = emailService;
        }

        public async Task<bool> CreateAdminAccAsync(CreateNewCustomerReqDto acc)
        {
            try
            {
                _logger.LogInformation("CreateAdminAccAsync: " + acc.Email);
                // Check conditions
                var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                var Adminrole = await _roleRepository.FindIdByRoleNameAsync(ActorEnumCode.ADMIN);
                if (
                    _http.HttpContext != null
                    && CheckTokenIsExpires(_http.HttpContext.Request.Headers["Authorization"].ToString()) == true
                    && userLogged.RoleId != Adminrole == false)
                {
                    throw new Exception(ErrorCode.NOT_AUTHORIZED);
                }
                if (!await _accountRepository.CheckEmailIsExist(acc.Email))
                {
                    throw new Exception(ErrorCode.USER_IS_EXIST);
                }
                // Create new account
                var passwordHash = CreatePasswordHash(acc.Password, out byte[] passwordSalt);
                var newAcc = new Account
                {
                    FirstName = acc.FirstName,
                    LastName = acc.LastName,
                    Email = acc.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    PhoneNumber = acc.PhoneNumber,
                    RoleId = await _roleRepository.FindIdByRoleNameAsync(ActorEnumCode.ADMIN),
                    StatusId = await _statusRepository.FindIdByStatusNameAsync(StatusAccount.ACTIVED),
                    Address = acc.Address
                };
                return await _accountRepository.CreateUserAsync(newAcc);                
            }
            catch (Exception)
            {
                throw;
            }
        }     

        public async Task<string> CreateMemberAccAsync(CreateNewCustomerReqDto acc)
        {
            try
            {
                _logger.LogInformation("CreateMemberAccAsync: " + acc.Email);
                if (await _accountRepository.CheckEmailIsExist(acc.Email))
                {
                    throw new Exception(ErrorCode.USER_IS_EXIST);
                }
                var passwordHash = CreatePasswordHash(acc.Password, out byte[] passwordSalt);

                // Create new otp code
                var otpCode = GenerateOtpCode();

                var newAcc = new Account
                {
                    FirstName = acc.FirstName,
                    LastName = acc.LastName,
                    Email = acc.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    PhoneNumber = acc.PhoneNumber,
                    RoleId = await _roleRepository.FindIdByRoleNameAsync(ActorEnumCode.CUSTOMER),
                    StatusId = await _statusRepository.FindIdByStatusNameAsync(StatusAccount.ACTIVED),
                    Address = acc.Address,
                    OtpCode = otpCode,
                    OtpCodeCreated = DateTime.UtcNow,
                    OtpCodeExpired = DateTime.UtcNow.AddMinutes(5)
                };

                // Send otp code to email
                var subject = "OTP Code";
                var body = "Your OTP Code is: " + otpCode;
                await _emailService.SendEmail(subject, body, acc.Email);
                
                if (await _accountRepository.CreateUserAsync(newAcc))
                {
                    return CreateBearerToken(newAcc);                    
                } 
                else
                {
                    throw new Exception(ErrorCode.SERVER_ERROR);
                }               
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
                var user = new Account();
                // Check login with email or phone number
                if (login.Email == null)
                {
                    user = await _accountRepository.GetUserByPhoneNumberAysnc(login.PhoneNumber);
                    if (user == null || !VerifyPasswordHash(login.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
                        throw new Exception(ErrorCode.USER_NOT_FOUND);
                    return CreateBearerToken(user);
                } 
                _logger.LogInformation("LoginAsync: "+ login.Email);
                user = await _accountRepository.GetUserByEmailAysnc(login.Email);
                // Check conditions when user not found, password not match 
                if (user == null || !VerifyPasswordHash(login.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt))) 
                    throw new Exception(ErrorCode.USER_NOT_FOUND);                
                return CreateBearerToken(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Validate account by otp code 
        public async Task<bool> ValidateAcccountByOtpCodeAsync(string optCode)
        {
            _logger.LogInformation("ValidateAcccountByOtpCodeAsync");
            try
            {
                // Get user by id
                var user = await _accountRepository.GetUserByIdAsync(GetSidLogged());                
                if (user == null)
                {
                    throw new Exception(ErrorCode.USER_NOT_FOUND);
                }
                if (user.OtpCode == null)
                {
                    throw new Exception(ErrorCode.OTP_CODE_NOT_FOUND);
                }
                if (user.OtpCode != optCode)
                {
                    throw new Exception(ErrorCode.OTP_CODE_NOT_MATCH);
                }
                if (user.OtpCodeExpired < DateTime.UtcNow)
                {
                    throw new Exception(ErrorCode.OTP_CODE_EXPIRED);
                }
                user.StatusId = await _statusRepository.FindIdByStatusNameAsync(StatusAccount.ACTIVED);
                user.OtpCode = null;
                user.OtpCodeCreated = null;
                user.OtpCodeExpired = null;
                return await _accountRepository.UpdateUserAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> GetStatusAccountAsync()
        {
            _logger.LogInformation("GetStatusAccountByBearerTokenAsync");
            try
            {
                var user = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                if (user == null)
                {
                    throw new Exception(ErrorCode.USER_NOT_FOUND);
                }
                if (user.StatusId == await _statusRepository.FindIdByStatusNameAsync(StatusAccount.ACTIVED))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetProfileCustomerReqDto> GetProfileCustomerAsync()
        {
            _logger.LogInformation("GetProfileCustomerAsync");
            try
            {
                var user = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                if (user == null)
                {
                    throw new Exception(ErrorCode.USER_NOT_FOUND);
                }
                var result = new GetProfileCustomerReqDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,                    
                };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Create new otp code and send to email case user forgot password or want to change password
        public async Task<bool> GenerateOtpAccountAndSendToEmail()
        {
            _logger.LogInformation("GenerateOtpAccountAndSendToEmail");
            var user = await _accountRepository.GetUserByIdAsync(GetSidLogged());
            if (user == null)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
            var otpCode = GenerateOtpCode();
            user.OtpCode = otpCode;
            user.OtpCodeCreated = DateTime.UtcNow;
            user.OtpCodeExpired = DateTime.UtcNow.AddMinutes(5);
            await _accountRepository.UpdateUserAsync(user);
            var subject = "OTP Code";
            var body = "Your OTP Code is: " + otpCode;
            await _emailService.SendEmail(subject, body, user.Email);
            return true;           
        }

        public async Task<List<Account>> GetAllAccountsWithRoleAsync(string roleEnum)
        {
            roleEnum = roleEnum.ToUpper();
            var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
            if(userLogged == null)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
            var userRole = await _roleRepository.GetRoleByIdAsync(userLogged.RoleId);
            if(userRole == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }    
            var list = await Authorization(userRole.RoleName,roleEnum);
            return list;
        }


        public async Task<string> GetRoleNameByHttpsClientServiceAsync()
        {
            _logger.LogInformation("GetRoleNameByHttpsClientServiceAsync");
            try
            {
                var user = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                if (user == null)
                {
                    throw new Exception(ErrorCode.USER_NOT_FOUND);
                }
                var role = await _roleRepository.GetRoleByIdAsync(user.RoleId);
                if (role == null)
                {
                    throw new Exception(ErrorCode.ROLE_NOT_FOUND);
                }
                return role.RoleName;
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
            var sid = _http.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
            if (sid == null)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
            return Guid.Parse(sid);            
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
        
        private string CreateBearerToken(Account logUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, logUser.Id.ToString())                
            };
#pragma warning disable CS8604 // Possible null reference argument.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
#pragma warning restore CS8604 // Possible null reference argument.
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
#pragma warning disable CS8604 // Possible null reference argument.
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
#pragma warning restore CS8604 // Possible null reference argument.
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),                
                ValidateIssuer = false,
                ValidateAudience = false                
            }, out SecurityToken validatedToken);
            return validatedToken.ValidTo < DateTime.UtcNow;
        }        

        // Create otp code
        private string GenerateOtpCode()
        {
            var otpCode = new Random().Next(100000, 999999).ToString();
            return otpCode;
        }        
        private async Task<List<Account>> Authorization(string userRole, string roleNameToFind)
        {
            switch(userRole)
            {
                case ActorEnumCode.ADMIN:
                    var roleId1 = await _roleRepository.FindIdByRoleNameAsync(roleNameToFind);
                    return await _accountRepository.GetAllAccountsWithRoleAsync(roleId1);
                   
                case ActorEnumCode.SHIPPER_MANAGER:
                    if(roleNameToFind == ActorEnumCode.SHIPPER_MANAGER)
                    {
                        var roleId2 = await _roleRepository.FindIdByRoleNameAsync(roleNameToFind);
                        return await _accountRepository.GetAllAccountsWithRoleAsync(roleId2);
                    }
                    throw new Exception(ErrorCode.NOT_ALLOW);
                default:
                    throw new Exception(ErrorCode.NOT_ALLOW);
            }
        }
        #endregion
    }
}
