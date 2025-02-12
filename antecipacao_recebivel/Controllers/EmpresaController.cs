using antecipacao_recebivel.Data;
using antecipacao_recebivel.DataAccess;
using antecipacao_recebivel.Models;
using antecipacao_recebivel.Rules;
using antecipacao_recebivel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace antecipacao_recebivel.Controllers
{
    [ApiController]
    [Route("api/empresa")]
    public class EmpresaController : ControllerBase
    {
   
        private readonly ActionsNotaFiscal _actionsNotaFiscal;
        private readonly NfRepository _nfRepository;
        private readonly EmpresaRepo _empresaRepo;

        public EmpresaController( ActionsNotaFiscal actionsNotaFiscal, NfRepository nfRepository, EmpresaRepo empresaRepo)
        {
            
            _actionsNotaFiscal = actionsNotaFiscal;
            _nfRepository = nfRepository;
            _empresaRepo = empresaRepo;

        }
        
        [HttpPost("cadempresa")]
        public IActionResult cadastrarEmpresa([FromBody] Empresa empresa)
        {

            var resultado = _empresaRepo.existeEmpresa(empresa);

            return Ok(resultado);

        }      

        [HttpPost("{cnpj}/cadNotaFiscal")]
        public IActionResult cadastrarNotaFiscal([FromBody] NotasFiscais notasFiscais, string cnpj)
        {
            var resultado = _actionsNotaFiscal.validaNotaFiscal(notasFiscais, cnpj);

            return Ok(resultado);
        }

        [HttpGet("{cnpj}/listaNotaFiscal")]
        public IActionResult ListarNotas(string cnpj)
         {
             var resultado = _actionsNotaFiscal.buscaNF(cnpj);
            // var notas = _dbContext.NotasFiscais.Where(n => n.IdEmpresa == empresa.Id).ToList();
            return Ok(resultado);
        }

        [HttpPost("{cnpj}/calcLimite")]
        public IActionResult calcularLimite([FromBody] List<NotasFiscais> notas, string cnpj)
        {

            DateTime dataAtual = DateTime.Now;
            decimal totalLiquido = 0m;
            decimal totalBruto = 0m;
            List<object> notasFiscais = new List<object>();

            foreach (var nota in notas)
            {
                int prazo = (nota.datavencimento - dataAtual).Days;

                if (prazo < 0)
                {
                    return BadRequest(new { sucesso = false, mensagem = "A data de vencimento não pode ser anterior à data atual." });
                }

                decimal valorLiquido = _nfRepository.CalculaDesagio(nota.valor, nota.datavencimento, dataAtual);

                notasFiscais.Add(new
                {
                    numero = nota.numero,
                    valor_bruto = nota.valor,
                    valor_liquido = Math.Round(valorLiquido, 2)
                });

                totalLiquido += valorLiquido;
                totalBruto += nota.valor;
            }

            Empresa dataempresa = _empresaRepo.GetEmpresaPorCnpj(cnpj);
            decimal limiteAntecipacao = _nfRepository.CalcularLimiteAntecipacao(dataempresa.faturamento, dataempresa.ramo);

            return Ok(new
            {
                empresa = dataempresa.nome,
                cnpj = cnpj,
                limite = Math.Round(limiteAntecipacao, 0),
                notas_fiscais = notasFiscais,
                total_liquido = Math.Round(totalLiquido, 0),
                total_bruto = totalBruto
            });
        }
    }
}

