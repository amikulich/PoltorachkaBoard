using System.ComponentModel.DataAnnotations;

namespace Poltorachka.Models
{
    public class CreateFactModel
    {
        [Required(ErrorMessage = "Select name")]
        public string WinnerName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select name")]
        public string LoserName { get; set; }

        [Required(ErrorMessage = "Score must be between 1 to 255")]
        [Range(1, byte.MaxValue)]
        public byte Score { get; set; }

        [MaxLength(255, ErrorMessage = "Description must be less then 255 symbols")]
        public string Description { get; set; }
    }
}