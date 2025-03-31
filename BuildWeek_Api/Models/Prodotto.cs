using System.ComponentModel.DataAnnotations;

namespace BuildWeek_Api.Models
{
    public class Prodotto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(50)]
        public string DittaFornitrice  { get; set; }

        [Required]
        [StringLength(1000)]
        public string ProdottoUso { get; set; }

        public PosizioneMedicinale? PosizioneMedicinale { get; set; }
        public ICollection<Vendita> Vendite { get; set; }
    }

}
