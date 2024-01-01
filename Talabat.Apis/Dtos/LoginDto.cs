using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }    
    }
}
