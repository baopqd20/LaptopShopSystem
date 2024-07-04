using System.ComponentModel.DataAnnotations;

namespace LaptopShopSystem.Dto
{
    public class UserLoginDto
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters")]
        public string Password { get; set; }
    }
}
