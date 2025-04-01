namespace BuildWeek_Api.DTOs.Ricovero
{
    public class RicoveroCreateDto
    {
        public DateTime DataInizio { get; set; }
        public string? Tipologia { get; set; }
        public string? ColoreMantello { get; set; }
        public bool? MicrochipPresente { get; set; }
        public string? NumeroMicrochip { get; set; }
        public string Descrizione { get; set; }
        public Guid? AnimaleId { get; set; }
    }
}
