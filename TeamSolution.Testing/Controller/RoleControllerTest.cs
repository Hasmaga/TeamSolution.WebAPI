using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSolution.Model.Dto;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Testing.Controller
{
    public class RoleControllerTest 
    {
        private readonly Mock<IRoleReposotory> _mockRoleRepo;
        
        public RoleControllerTest() 
        { 
            _mockRoleRepo = new Mock<IRoleReposotory>();
        }

        [Fact]
        public async Task CreateRoleAsync_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var mockRepo = new Mock<IRoleReposotory>();           

            // Act
            _mockRoleRepo.Setup(repo => repo.CreateRoleAsync(It.IsAny<NewRoleReqDto>()))
                .ReturnsAsync(true);
            var result = await _mockRoleRepo.Object.CreateRoleAsync(new NewRoleReqDto());            
            // Assert            
            Assert.True(result);
        }
    }
}
