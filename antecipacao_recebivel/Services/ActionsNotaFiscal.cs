using antecipacao_recebivel.Controllers;
using antecipacao_recebivel.DataAccess;
using antecipacao_recebivel.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace antecipacao_recebivel.Services
{
    public class ActionsNotaFiscal
    {
        private readonly NfRepository _nfrepo;

        public ActionsNotaFiscal(NfRepository nfRepository)
        {
            _nfrepo = nfRepository;
        }

        public Resultado validaNotaFiscal(NotasFiscais notasFiscais, string cnpj)
        {
            if (_nfrepo.existeNF(notasFiscais.numero, cnpj))
                return new Resultado(false, "Nota Fiscal Já Cadastrada!");

            _nfrepo.adicionarNF(notasFiscais, cnpj);
            return new Resultado(true, "Nota Fiscal cadastrada com sucesso!");
        }

        public Resultado buscaNF (string cnpj)
        {
            var notas = _nfrepo.listaNF(cnpj);
            if (notas == null || !notas.Any())
                return new Resultado(false, "Nenhuma Nota Fiscal Encontrada!");

            return new Resultado(true, JsonSerializer.Serialize(notas));
            
        }

       
    }
}
