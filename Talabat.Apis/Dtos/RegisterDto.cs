using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Dtos
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; } 

        public string Password { get; set; } 

    }
}
