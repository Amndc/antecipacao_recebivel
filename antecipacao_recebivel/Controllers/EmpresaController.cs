using antecipacao_recebivel.Data;
using antecipacao_recebivel.Models;
using antecipacao_recebivel.Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace antecipacao_recebivel.Controllers
{
    public class EmpresaController : ControllerBase
    {
        private readonly ActionsEmpresa _actionsEmpresa;       

        public EmpresaController(ActionsEmpresa actionsEmpresa)
        {
            _actionsEmpresa = actionsEmpresa;        
        }

        [Route("cadempresa")]       
        [HttpPost]
        public IActionResult cadastrarEmpresa([FromBody] Empresa empresa)
        {

            var resultado = _actionsEmpresa.validaEmpresa(empresa);

            return Ok(resultado);            
        }

        [Route("cadNotaFiscal")]
        [HttpPost]
        public IActionResult cadastrarNotaFiscal([FromBody] Empresa empresa)
        {

            var resultado = _actionsEmpresa.validaEmpresa(empresa);

            return Ok(resultado);
        }
    }
}
