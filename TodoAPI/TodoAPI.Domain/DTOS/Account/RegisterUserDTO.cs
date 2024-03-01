
using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Domain.DTO.Account
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "You must Enter Full name")]
        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Name should only contains letters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "You must Enter the Email")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Enter a valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must Enter the phone number")]
        [RegularExpression("^(010|012|011|015)\\d{8}$", ErrorMessage = "The format is not supported, it should start with on of the following, {010,012,011,015}")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "You must Enter the Password")]
        //[RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&\\s])[A-Za-z\\d@$!%*#?&\\s]{8,}$", ErrorMessage = "Password must enter at least one Upper case letter, degit, special chaaracters, and at least 8 length")]
        public string Password { get; set; }

    }
}
