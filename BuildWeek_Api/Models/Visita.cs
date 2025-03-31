using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek_Api.Models
{
    public class Visita
    {
        [Key]
        public int VisitaId { get; set; }

        [Required]
        public DateTime DataVisita { get; set; }

        [Required]
        public string EsameObiettivo { get; set; }

        [Required]
        public string CuraPrescritta { get; set; }

        [Required]
        public int AnimaleId { get; set; }

        [ForeignKey("AnimaleId")]
        public Animale? Animale { get; set; }
    }
}
