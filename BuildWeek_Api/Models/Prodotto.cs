using System.ComponentModel.DataAnnotations;

namespace BuildWeek_Api.Models
{
    public class Prodotto
    {
        [Key]
        public Guid ProdottoId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nome { get; set; } 

        [Required]
        [StringLength(50)]
        public required string Tipo { get; set; }

        [Required]
        [StringLength(50)]
        public required string DittaFornitrice  { get; set; }

        [Required]
        [StringLength(1000)]
        public required string ProdottoUso { get; set; }

        [Required]
        public Guid PosizioneId { get; set; }
        public Posizione Posizione { get; set; } = null!;

        public ICollection<Vendita> Vendite { get; set; }
    }

}
