using System.ComponentModel.DataAnnotations;

namespace BuildWeek_Api.Models
{
    public class Animale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required DateTime DataRegistrazione { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Nome { get; set; }

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

        public ICollection<Visita>? Visite { get; set; }
        public ICollection<Ricovero>? Ricoveri { get; set; }
    }
}
