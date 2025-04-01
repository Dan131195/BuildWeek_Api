namespace BuildWeek_Api.DTOs.Ricovero
{
    public class RicoveroReadDto
    {
        public Guid RicoveroId { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string? Tipologia { get; set; }
        public string? ColoreMantello { get; set; }
        public bool? MicrochipPresente { get; set; }
        public string? NumeroMicrochip { get; set; }
        public string Descrizione { get; set; }
        public Guid? AnimaleId { get; set; }
        public string? NomeAnimale { get; set; }
    }
}
