using System.ComponentModel.DataAnnotations;

namespace BuildWeek_Api.Models
{
    public class Posizione
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CodiceArmadietto { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string NumeroCassetto { get; set; } = null!;


        public Prodotto Prodotto { get; set; } = null!;
    }
}
