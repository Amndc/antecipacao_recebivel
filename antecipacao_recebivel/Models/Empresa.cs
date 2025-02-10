namespace antecipacao_recebivel.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string cnpj { get; set; }
        public string nome { get; set; }
        public decimal faturamentoMensal { get; set; }
        public int ramo { get; set; }
    }


}
