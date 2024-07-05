namespace TechNationFinanceiroApi.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public string? NomePagador { get; set; }
        public string? NumeroIdentificacao { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataCobranca { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal? Valor { get; set; }
        public string? DocumentoFiscal { get; set; }
        public string? DocumentoBoleto { get; set; }
        public string? Status { get; set; }
    }
}
