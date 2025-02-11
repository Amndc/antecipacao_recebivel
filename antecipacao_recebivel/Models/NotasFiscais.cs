namespace antecipacao_recebivel.Models
{
    public class NotasFiscais
    {
        public int idnotafiscal { get; set; }
        public DateOnly  datavencimento { get; set; }     
        public string numero { get; set; }   
    }
}
