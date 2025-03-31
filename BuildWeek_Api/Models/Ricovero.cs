using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek_Api.Models
{
    public class Ricovero
    {
        [Key]
        public Guid RicoveroId { get; set; }

        [Required]
        public required DateTime DataInizio { get; set; }

        public string? Tipologia { get; set; }

        public string? ColoreMantello { get; set; }

        public bool? MicrochipPresente { get; set; }

        public string? NumeroMicrochip { get; set; }

        public string? Nome { get; set; } 

        public DateTime? DataNascita { get; set; }

        [Required]
        public required string Descrizione { get; set; }

        public Guid? AnimaleId { get; set; }

        [ForeignKey("AnimaleId")]
        public Animale? Animale { get; set; }
    }
}
