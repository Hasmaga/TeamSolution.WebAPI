﻿using TeamSolution.Model;
using TeamSolution.ViewModel.Account;

namespace TeamSolution.Service.Interface
{
    public interface IAccountService
    {
        Task<bool> CreateAdminAccAsync(CreateNewCustomerReqDto acc);
        Task<string> CreateMemberAccAsync(CreateNewCustomerReqDto acc);
        Task<string> LoginAsync(LoginReqDto loginReqDto);
        Task<bool> ValidateAcccountByOtpCodeAsync(string otpCode);
        Task<bool> GetStatusAccountAsync();
        Task<GetProfileCustomerReqDto> GetProfileCustomerAsync();
        Task<bool> GenerateOtpAccountAndSendToEmail();
        Task<bool> UpdateProfileUserAsync(UpdateProfileCustomerReqDto model);
        Task<List<Account>> GetShippersByRoleAndAvailabilityAsync(string roleName);
    }
}
