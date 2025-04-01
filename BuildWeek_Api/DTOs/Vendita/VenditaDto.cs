namespace BuildWeek_Api.DTOs
{
    public class VenditaDTO
    {
        public Guid? Id { get; set; } 

        public Guid AnimaleId { get; set; }

        public string CodiceFiscaleCliente { get; set; }

        public Guid ProdottoId { get; set; }

        public string? NumeroRicetta { get; set; }

        public DateTime? DataVendita { get; set; } 
    }
}
