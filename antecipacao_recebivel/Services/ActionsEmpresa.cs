using antecipacao_recebivel.DataAccess;
using antecipacao_recebivel.Models;
using System.Reflection.Metadata.Ecma335;

namespace antecipacao_recebivel.Rules
{
    public class ActionsEmpresa
    {
        private readonly EmpresaRepo _empresaRepo;

        public ActionsEmpresa(EmpresaRepo empresaRepo)
        {
            _empresaRepo = empresaRepo;
        }
        public Resultado validaEmpresaExiste(Empresa empresa)
        {
            if(_empresaRepo.existeEmpresa(empresa.cnpj))
                return new Resultado(false, "Empresa já cadastrada, Deseja Visualizar os Dados?");

            _empresaRepo.adicionarEmpresa(empresa);
            return new Resultado(true, "Empresa cadastrada com sucesso!");
        }
        


    }       
}
