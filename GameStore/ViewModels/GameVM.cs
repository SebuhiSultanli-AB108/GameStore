using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class GameVM
    {
        [MaxLength(64), Required]
        public string Name { get; set; } = null!;
        public IFormFile Image { get; set; }
        [Required]
        public double Price { get; set; }
        [MaxLength(528), Required]
        public string Description { get; set; } = null!;
        [MaxLength(16), Required]
        public string GameId { get; set; } = null!;
    }
}
