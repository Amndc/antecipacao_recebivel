using antecipacao_recebivel.DataAccess;
using antecipacao_recebivel.Models;

namespace antecipacao_recebivel.Services
{
    public class ActionsNotaFiscal
    {
        private readonly NfRepository _nfrepo;

        public ActionsNotaFiscal(NfRepository nfRepository)
        {
            _nfrepo = nfRepository;
        }
        public Resultado validaEmpresa(NotasFiscais notasFiscais)
        {
            if (_nfrepo.existeNF(notasFiscais.numero))
                return new Resultado(false, "Empresa já cadastrada!");

            _nfrepo.adicionarNF(notasFiscais);
            return new Resultado(true, "Empresa cadastrada com sucesso!");
        }
    }
}
