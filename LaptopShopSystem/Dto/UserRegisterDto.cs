namespace LaptopShopSystem.Dto
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
