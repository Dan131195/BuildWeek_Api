using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek_Api.Models
{
    public class Ricovero
    {
        [Key]
        public int RicoveroId { get; set; }

        [Required]
        public required DateTime DataInizio { get; set; }

        [Required]
        public required string Tipologia { get; set; }

        [Required]
        public required string ColoreMantello { get; set; }

        [Required]
        public required bool MicrochipPresente { get; set; }

        public string? NumeroMicrochip { get; set; }

        public string? Nome { get; set; } 

        public DateTime? DataNascita { get; set; }

        [Required]
        public required string Descrizione { get; set; }

        public int? AnimaleId { get; set; }

        [ForeignKey("AnimaleId")]
        public Animale? Animale { get; set; }
    }
}
