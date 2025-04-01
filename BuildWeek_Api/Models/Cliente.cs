using System.ComponentModel.DataAnnotations;
using BuildWeek_Api.Models.Auth;

namespace BuildWeek_Api.Models
{
    public class Cliente
    {
        [Key]
        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        public ICollection<Vendita> Vendite { get; set; }
        public ICollection<Animale> Animali { get; set; }
    }
}
