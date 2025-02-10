using antecipacao_recebivel.Data;
using antecipacao_recebivel.Models;

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

        public Empresa ObterPorCNPJ(string cnpj)
        {
            //TODO 
           //Mudar pra CNPJ
            return _DBrecebivel.Empresas.FirstOrDefault(e => e.cnpj == cnpj);
        }

        //valida se empresa ja existe - Mudar pro banco?
        public bool existsEmpresa (string cnpj)
        {
            return _DBrecebivel.Empresas.Any(e => e.cnpj == cnpj);
        }
    }
}
