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

        [Route("empresa")]       
        [HttpPost]
        public IActionResult CadastrarEmpresa([FromBody] Empresa empresa)
        {

            var resultado = _actionsEmpresa.CadEmpresa(empresa);

             return Ok(resultado);            
        }
    }
}
