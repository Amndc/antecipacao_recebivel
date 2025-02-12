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
        private readonly ActionsEmpresa _actionsEmpresa;
        private readonly ActionsNotaFiscal _actionsNotaFiscal;
        private readonly NfRepository _nfRepository;
        private readonly EmpresaRepo _empresaRepo;

        public EmpresaController(ActionsEmpresa actionsEmpresa, ActionsNotaFiscal actionsNotaFiscal, NfRepository nfRepository, EmpresaRepo empresaRepo)
        {
            _actionsEmpresa = actionsEmpresa;
            _actionsNotaFiscal = actionsNotaFiscal;
            _nfRepository = nfRepository;
            _empresaRepo = empresaRepo;

        }
        
        [HttpPost("cadempresa")]
        public IActionResult cadastrarEmpresa([FromBody] Empresa empresa)
        {

            var resultado = _actionsEmpresa.validaEmpresaExiste(empresa);

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
            decimal limiteTotal = 0m;
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
                    valor_liquido = valorLiquido
                });

                // Acumula os totais
                totalLiquido += valorLiquido;
                totalBruto += nota.valor;

                

            }
            string nomeEmpresa = _empresaRepo.GetNomeEmpresaPorCnpj(cnpj);
            // Retorna o valor total do limite calculado
            return Ok(new
            {
                empresa = nomeEmpresa,
                cnpj = cnpj,
                limite = totalLiquido, // Esse é o limite calculado
                notas_fiscais = notasFiscais,
                total_liquido = totalLiquido,
                total_bruto = totalBruto
            });
        }
    }
}

