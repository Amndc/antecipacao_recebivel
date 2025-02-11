using antecipacao_recebivel.Data;
using antecipacao_recebivel.Models;

namespace antecipacao_recebivel.DataAccess
{
    public class NfRepository
    {
        private readonly DbContextRecebivel _DBrecebivel;

        public NfRepository(DbContextRecebivel DBrecebivel)
        {
            _DBrecebivel = DBrecebivel;
        }

        public void adicionarNF(NotasFiscais notafiscal)
        {
            _DBrecebivel.NotasFiscais.Add(notafiscal);
            _DBrecebivel.SaveChanges();
        }

        public bool existeNF(string numero)
        {
            if (_DBrecebivel.NotasFiscais.Any(e => e.numero == numero))
                return true;

            return false;
        }
    }
}
