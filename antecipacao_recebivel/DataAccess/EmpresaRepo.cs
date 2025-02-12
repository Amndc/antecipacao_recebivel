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
      
        public Resultado existeEmpresa (Empresa empresa)
        {
            var empresaExistente = GetEmpresaPorCnpj(empresa.cnpj);


            if (empresaExistente != null)
            {
                if (!string.Equals(empresaExistente.nome, empresa.nome, StringComparison.OrdinalIgnoreCase))
                {            
                    return new Resultado(false, "CNPJ já cadastrado, mas o nome informado não corresponde ao registro existente.");
                }
                else
                {                   
                    return new Resultado(false, "CNPJ já cadastrado. O que deseja fazer?");
                }

            }

            adicionarEmpresa(empresa);
            return new Resultado(true, "Empresa cadastrada com sucesso!");           
        }
        public enum EmpresaValidationStatus
        {
            EmpresaNaoEncontrada,      
            EmpresaValida,             
            EmpresaInvalidaPorDados    
        }

        

        public Empresa GetEmpresaPorCnpj(string cnpj)
        {
            var empresa = _DBrecebivel.Empresas.FirstOrDefault(e => e.cnpj == cnpj);
            

            return empresa ?? throw new Exception("Empresa não encontrada");
        }
    }
}
