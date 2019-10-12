using System.ComponentModel.DataAnnotations;

namespace betauia.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = "204")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password,ErrorMessage = "604")]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "605")]
        public string ConfirmPassword { get; set; }
    }
}