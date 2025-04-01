namespace BuildWeek_Api.DTOs.Prodotto
{
    public class ProdottoDto
    {
        public Guid ProdottoId { get; set; }
        public required string Nome { get; set; }
        public required string Tipo { get; set; }
        public required string DittaFornitrice { get; set; } 
        public required string ProdottoUso { get; set; }

        public Guid? PosizioneId { get; set; }
        public string CodiceArmadietto { get; set; } = null!;
        public string NumeroCassetto { get; set; } = null!;
    }
}
