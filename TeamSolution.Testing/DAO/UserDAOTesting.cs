using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSolution.DAO;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Testing.DAO
{
    public class UserDAOTesting
    {        
        private readonly ApplicationDbContext _mockDbcontext;
        private readonly UserDAO _userDAO;

        public UserDAOTesting()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDatabase")
                .Options;
            _mockDbcontext = new ApplicationDbContext(options);
            _userDAO = new UserDAO(_mockDbcontext);
        }

        [Fact]
        public async Task CreateUserAsync_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            var user = new User(
                "test",
                "test",
                "test",
                CreatePasswordHash("test", out byte[] passwordSalt),
                Convert.ToBase64String(passwordSalt),
                "test",
                true,
                0,
                Guid.Parse("48C1D11F-FE0C-4D02-825F-05486D4C543B"),
                Guid.Parse("48C1D21F-FE0C-4D02-825F-05486D4B543B"),
                "test",
                0,
                false,
                DateTime.Now,
                DateTime.Now,
                Guid.Parse("47C1D11F-FE0C-4D02-825F-05486D4B543B")
            );
            //Act
            await _userDAO.CreateUserAsync(user);
            var result = await _mockDbcontext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            //Assert
            Assert.Equal(user.Email, result.Email);      
        }

        [Fact]
        public async Task GetUserByEmailAsync_WhenCalled_ReturnEqualWithTestUser()
        {
            // Arrange
            var userTest = new User(
                "test",
                "test",
                "test",
                CreatePasswordHash("test", out byte[] passwordSalt),
                Convert.ToBase64String(passwordSalt),
                "test",
                true,
                0,
                Guid.Parse("48C1D11F-FE0C-4D02-825F-05486D4C543B"),
                Guid.Parse("48C1D21F-FE0C-4D02-825F-05486D4B543B"),
                "test",
                0,
                false,
                DateTime.Now,
                DateTime.Now,
                Guid.Parse("47C1D11F-FE0C-4D02-825F-05486D4B543B")
            );
            //Act
            await _mockDbcontext.Users.AddAsync(userTest);
            await _mockDbcontext.SaveChangesAsync();
            var userMock = await _userDAO.GetUserByEmailAysnc(userTest.Email);
            // Assett
            Assert.Equal(userTest, userMock);
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
