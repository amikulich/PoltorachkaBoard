using System.ComponentModel.DataAnnotations;

namespace Poltorachka.Web.Models
{
    public class FactCreateViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Нужно выбрать")]
        public int WinnerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Нужно выбрать")]
        public int LoserId { get; set; }

        [Range(1, 4, ErrorMessage = "Нельзя предъявить больше 4 за раз")]
        public byte Score { get; set; }

        [MaxLength(255, ErrorMessage = "Давай-ка до 255 буков")]
        public string Description { get; set; }
    }
}