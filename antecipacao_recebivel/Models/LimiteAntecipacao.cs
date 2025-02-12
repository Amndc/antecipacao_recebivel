namespace antecipacao_recebivel.Models
{
    public class LimiteAntecipacao
    {
        public int id { get; set; }
        public decimal faixaMin { get; set; }
        public decimal faixaMax { get; set; }
        public int porcentagem { get; set; }
        public int ramo { get; set; }
    }
}