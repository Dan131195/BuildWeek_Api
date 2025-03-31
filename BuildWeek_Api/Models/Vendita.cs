using System.ComponentModel.DataAnnotations;

namespace BuildWeek_Api.Models
{
    public class Vendita
    {
        public Guid Id { get; set; }
        [Required]
        public Guid AnimaleId { get; set; }

        [Required]
        public required string CodiceFiscaleCliente { get; set; }

        [Required]
        public Guid ProdottoId { get; set; }

        [Required]
        public DateTime DataVendita { get; set; }

        [Required]
        public string? NumeroRicetta { get; set; } 

        public Cliente Cliente { get; set; }
        public Prodotto Prodotto { get; set; }
        public Animale Animale { get; set; }
    }
}
