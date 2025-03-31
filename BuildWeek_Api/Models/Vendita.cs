namespace BuildWeek_Api.Models
{
    public class Vendita
    {
        public int Id { get; set; }
        public string ClienteCodiceFiscale { get; set; } = null!;
        public int ProdottoId { get; set; }
        public DateTime DataVendita { get; set; }
        public string? NumeroRicetta { get; set; } // solo per medicinali

        public Cliente Cliente { get; set; } = null!;
        public Prodotto Prodotto { get; set; } = null!;
    }
}
