using antecipacao_recebivel.Data;
using antecipacao_recebivel.Models;
using Newtonsoft.Json;

namespace antecipacao_recebivel.DataAccess
{
    public class EmpresaRepo
    {
        private readonly DbContextRecebivel _DBrecebivel;


        public EmpresaRepo(DbContextRecebivel DBrecebivel)
        {
            _DBrecebivel = DBrecebivel;
        }

        public void Adicionar(Empresa empresa)
        {
            _DBrecebivel.Empresas.Add(empresa);
            _DBrecebivel.SaveChanges();
        }
      
        public bool existeEmpresa (string cnpj)
        {
            if (_DBrecebivel.Empresas.Any(e => e.cnpj == cnpj))
                return true;

            return false;


        }
    }
}
