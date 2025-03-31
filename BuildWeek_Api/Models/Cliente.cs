using System.ComponentModel.DataAnnotations;

namespace BuildWeek_Api.Models
{
    public class Cliente
    {
        [Key]
        public string CodiceFiscale { get; set; } 
        public string? Nome { get; set; }
        public string? Cognome { get; set; }

        public ICollection<Vendita> Vendite { get; set; } 
    }
}
