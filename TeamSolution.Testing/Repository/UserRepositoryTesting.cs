using Microsoft.EntityFrameworkCore;
using System.Text;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using TeamSolution.Repository;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Testing.DAO
{
    public class UserRepositoryTesting
    {        
        private readonly ApplicationDbContext _mockDbcontext;
        private readonly IAccountRepository _accountRepository;
        public UserRepositoryTesting()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDatabase")
                .Options;
            _mockDbcontext = new ApplicationDbContext(options);
            _accountRepository = new AccountRepository(_mockDbcontext);
        }

        [Fact]
        public async Task CreateUserAsync_WhenCalled_ReturnsOkResult()
        {
            //Arrange           

            var user = new Account
            {
                FirstName = "test",
                LastName = "test",
                Email = "test@gmail.com",
                PasswordHash = CreatePasswordHash("test", out byte[] passwordSalt),
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                PhoneNumber = "0123456789",
                RoleId = Guid.Parse("48C1D11F-FE0C-4D02-825F-05486D4C543B"),
                StatusId = Guid.Parse("45C1D21F-FE0C-4D02-825F-05486D4B543B"),
                Address = "test"
            };
            //Act
            await _accountRepository.CreateUserAsync(user);
            var result = await _mockDbcontext.Accounts.FirstOrDefaultAsync(x => x.Email == user.Email);
            //Assert
            Assert.Equal(user.Email, result.Email);      
        }

        [Fact]
        public async Task GetUserByEmailAsync_WhenCalled_ReturnEqualWithTestUser()
        {
            // Arrange
            var user = new Account
            {
                FirstName = "test",
                LastName = "test",
                Email = "test@gmail.com",
                PasswordHash = CreatePasswordHash("test", out byte[] passwordSalt),
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                PhoneNumber = "0123456789",
                RoleId = Guid.Parse("48C1D11F-FE0C-4D02-825F-05486D4C543B"),
                StatusId = Guid.Parse("45C1D21F-FE0C-4D02-825F-05486D4B543B"),
                Address = "test"
            };
            //Act
            await _mockDbcontext.Accounts.AddAsync(user);
            await _mockDbcontext.SaveChangesAsync();
            var userMock = await _accountRepository.GetUserByEmailAysnc(user.Email);
            // Assett
            Assert.Equal(user, userMock);
        }

        #region Private Method
        private string CreatePasswordHash(string password, out byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(passwordHash);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
