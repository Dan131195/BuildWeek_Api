namespace BuildWeek_Api.DTOs.Prodotto
{
    public class ProdottoCreateDto
    {
        public required string Nome { get; set; } 
        public required string Tipo { get; set; }
        public required string DittaFornitrice { get; set; } 
        public required string ProdottoUso { get; set; }
        public string CodiceArmadietto { get; set; }
        public string NumeroCassetto { get; set; }
    }
}
