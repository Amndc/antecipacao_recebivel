using antecipacao_recebivel.Models;
using antecipacao_recebivel.Rules;
using Microsoft.AspNetCore.Mvc;


namespace antecipacao_recebivel.Controllers
{
    [ApiController] 
    [Route("api/cadEmpresa")] 
    public class EmpresaController : ControllerBase
    {
        private readonly ActionsEnterprise _actionsEnterprise;

        public EmpresaController(ActionsEnterprise actiosnEnterprise)
        {
            _actionsEnterprise = actiosnEnterprise;
        }

        [HttpPost] 
        public IActionResult RegisterEnterprise([FromBody] Empresa enterprise)
        {
            var resut = _actionsEnterprise.RegisterEnterprise(enterprise);
            
            return Ok(resut);
        }
    }
}
