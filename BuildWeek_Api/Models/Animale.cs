using System.ComponentModel.DataAnnotations;

namespace BuildWeek_Api.Models
{
    public class Animale
    {
        [Key]
        public Guid AnimaleId { get; set; }

        [Required]
        public required DateTime DataRegistrazione { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        public required string NomeAnimale { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Tipologia { get; set; }

        [MaxLength(50)]
        public required string ColoreMantello { get; set; }

        public DateTime? DataNascita { get; set; }

        [Required]
        public required bool MicrochipPresente { get; set; }

        [MaxLength(50)]
        public string? NumeroMicrochip { get; set; }

        [MaxLength(50)]
        public string? NomeProprietario { get; set; }

        [MaxLength(50)]
        public string? CognomeProprietario { get; set; }

        [MaxLength(50)]
        public string? CodiceFiscaleProprietario {  get; set; }
        public Cliente? Proprietario { get; set; } 

        public ICollection<Visita>? Visite { get; set; }
        public ICollection<Ricovero>? Ricoveri { get; set; }
        public ICollection<Vendita>? Vendite { get; set; }
    }
}
