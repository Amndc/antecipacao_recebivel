using antecipacao_recebivel.Data;
using antecipacao_recebivel.Models;
using Microsoft.EntityFrameworkCore;
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

        public void adicionarEmpresa(Empresa empresa)
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

        public string GetNomeEmpresaPorCnpj(string cnpj)
        {
            // Busca a empresa pelo CNPJ no banco de dados
            var empresa = _DBrecebivel.Empresas.FirstOrDefault(e => e.cnpj == cnpj);

            // Se a empresa for encontrada, retorna o nome, caso contrário, retorna uma string vazia ou uma mensagem de erro
            return empresa?.nome ?? "Empresa não encontrada";
        }
    }
}
