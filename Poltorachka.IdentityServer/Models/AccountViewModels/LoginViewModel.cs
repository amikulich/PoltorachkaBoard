using System.ComponentModel.DataAnnotations;

namespace Poltorachka.IdentityServer.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(128)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
