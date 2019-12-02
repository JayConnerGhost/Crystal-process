using System.ComponentModel.DataAnnotations;

namespace CrystalProcess.API.Requests
{
    public class UserForRegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "you must specify password between 4 and 8 charactors")]
        public string Password { get; set; }
    }
}