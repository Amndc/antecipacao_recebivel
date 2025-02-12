namespace antecipacao_recebivel.Models
{
    public class LimiteAntecipacao
    {
        public int id { get; set; }
        public decimal faixaMin { get; set; }
        public decimal faixaMax { get; set; }
        public decimal porcentagem { get; set; }
    }
}