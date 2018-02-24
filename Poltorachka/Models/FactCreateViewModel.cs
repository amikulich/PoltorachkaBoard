using System.ComponentModel.DataAnnotations;

namespace Poltorachka.Models
{
    public class FactCreateViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Select a name")]
        public int WinnerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a name")]
        public int LoserId { get; set; }

        [Range(1, 4, ErrorMessage = "Score must be between 1 to 4")]
        public byte Score { get; set; }

        [MaxLength(255, ErrorMessage = "Description must be less then 255 symbols")]
        public string Description { get; set; }
    }
}