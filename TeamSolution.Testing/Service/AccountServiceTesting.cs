﻿using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using TeamSolution.Repository;
using TeamSolution.Repository.Interface;
using TeamSolution.Service;
using TeamSolution.Testing.DAO;

namespace TeamSolution.Testing.Repository
{
    public class AccountServiceTesting
    {
        private readonly Mock<AccountService> _mockAccountService;
        private readonly UserRepositoryTesting _userDAOTesting;
        public AccountServiceTesting()
        {
            _mockAccountService = new Mock<AccountService>();
        }

        //[Fact]
        //public async Task CreateAdminAccAsync_WhenCalled_ReturnsOkResult()
        //{
        //    //Arrange
        //    var acc = new NewAccReqDto
        //    {
        //        FirstName = "Test",
        //        LastName = "Test",
        //        Email = "",
        //        Address = "Test",
        //        Password = "Test",
        //        PhoneNumber = "Test"
        //    };         
        //}


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
