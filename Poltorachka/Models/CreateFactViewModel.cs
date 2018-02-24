using System.ComponentModel.DataAnnotations;

namespace Poltorachka.Models
{
    public class CreateFactViewModel
    {
        [Required(ErrorMessage = "Select a name")]
        public int WinnerIndId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Select a name")]
        public int LoserIndId { get; set; }

        [Required(ErrorMessage = "Score must be between 1 to 4")]
        [Range(1, 4)]
        public byte Score { get; set; }

        [MaxLength(255, ErrorMessage = "Description must be less then 255 symbols")]
        public string Description { get; set; }
    }
}