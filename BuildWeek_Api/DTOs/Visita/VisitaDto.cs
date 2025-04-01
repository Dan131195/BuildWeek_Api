namespace BuildWeek_Api.DTOs
{
    public class VisitaDTO
    {
        public Guid VisitaId { get; set; }
        public DateTime DataVisita { get; set; }
        public string EsameObiettivo { get; set; }
        public string CuraPrescritta { get; set; }
        public Guid AnimaleId { get; set; }
    }
}
