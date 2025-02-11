namespace antecipacao_recebivel.Models
{
    public class Empresa
    {
        public int id { get; set; }
        public string cnpj { get; set; }
        public string nome { get; set; }
        public decimal faturamento{ get; set; }
        public int ramo { get; set; }
    }


}
