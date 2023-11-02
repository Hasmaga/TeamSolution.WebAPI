namespace TeamSolution.ViewModel.Account
{
    public class UpdateProfileCustomerReqDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }

}
