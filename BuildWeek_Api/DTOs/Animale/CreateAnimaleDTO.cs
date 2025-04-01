namespace BuildWeek_Api.DTOs.Animale
{
    public class CreateAnimaleDTO
{
    public DateTime DataRegistrazione { get; set; } = DateTime.Now;
    public string NomeAnimale { get; set; }
    public string Tipologia { get; set; }
    public string? ColoreMantello { get; set; }
    public DateTime? DataNascita { get; set; }
    public bool MicrochipPresente { get; set; }
    public string? NumeroMicrochip { get; set; }
    public string? NomeProprietario { get; set; }
    public string? CognomeProprietario { get; set; }
    public string? CodiceFiscaleProprietario { get; set; }
}
}
