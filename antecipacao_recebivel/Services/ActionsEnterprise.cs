using antecipacao_recebivel.DataAccess;
using antecipacao_recebivel.Models;

namespace antecipacao_recebivel.Rules
{
    public class ActionsEnterprise
    {
        private readonly EmpresaRepo _empresaRepo;

        public Result CadEmpresa(Empresa empresa)
        {
            if (_empresaRepo.existsEnterprise(empresa.CNPJ))
                return new Resultado(false, "Empresa já cadastrada!");

            _empresaRepo.Adicionar(empresa);
            return new Result(true, "Empresa cadastrada com sucesso!");
        }
    }
}
