namespace antecipacao_recebivel.Models
{
    public class Resultado
    {
        public bool Sucesso { get; }
        public string Mensagem { get; }

        public Resultado(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}
