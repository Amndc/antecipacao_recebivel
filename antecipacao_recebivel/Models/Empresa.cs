using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace antecipacao_recebivel.Models
{
    public class Empresa
    {
        public int id { get; set; }
        public string cnpj { get; set; }
        public string nome { get; set; }
        public int ramo { get; set; }
        public string faturamento { get; set; }



        [NotMapped] // Essa propriedade não será mapeada para o banco
        public decimal? FaturamentoDec
        {
            get => decimal.TryParse(faturamento, out var valor) ? valor : null;
            set => faturamento = value?.ToString("N2"); // Formata como dinheiro
        }
    }


}
