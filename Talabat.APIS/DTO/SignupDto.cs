using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTO
{
	public class SignupDto
	{
		[Required]
        public string DisplayName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one digit, and one special character.")]
		public string Password { get; set; }
		[Required]
		public string Phone { get; set; }
    }
}
