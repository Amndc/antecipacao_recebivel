namespace antecipacao_recebivel.Models
{
    public class NotasFiscais
    {
        public int idnotafiscal { get; set; }
        public DateTime datavencimento { get; set; }
        public decimal valor { get; set; }
        public string numero { get; set; }
        public int idempresa { get; set; }
    }
}
