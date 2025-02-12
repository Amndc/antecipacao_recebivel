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
                // Verifica se o nome é diferente
                if (!string.Equals(empresaExistente.nome, empresa.nome, StringComparison.OrdinalIgnoreCase) ||
                    empresaExistente.faturamento != empresa.faturamento ||
                    empresaExistente.ramo != empresa.ramo) 
                {
                    return new Resultado(false, "CNPJ já cadastrado, mas algumas informações não correspondem. Você quer atualizar os dados?");
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
            

            return empresa ?? null;
        }

        public Resultado AtualizarEmpresa(Empresa empresa)
        {
            var empresaExistente = GetEmpresaPorCnpj(empresa.cnpj);

            if (empresaExistente == null)
            {
                return new Resultado(false, "Empresa não encontrada. O CNPJ fornecido não está cadastrado.");
            }
            bool dadosAlterados = false;

            if (!string.Equals(empresaExistente.nome, empresa.nome, StringComparison.OrdinalIgnoreCase))
            {
                empresaExistente.nome = empresa.nome;
                dadosAlterados = true;
            }

            if (empresaExistente.faturamento != empresa.faturamento)
            {
                empresaExistente.faturamento = empresa.faturamento;
                dadosAlterados = true;
            }

            if (empresaExistente.ramo != empresa.ramo)
            {
                empresaExistente.ramo = empresa.ramo;
                dadosAlterados = true;
            }

            // Se houver alterações, atualize o banco de dados
            if (dadosAlterados)
            {
                _DBrecebivel.SaveChanges(); // Assume que você está usando EF ou outro ORM.
                return new Resultado(true, "Dados da empresa atualizados com sucesso!");
            }

            return new Resultado(false, "Nenhuma alteração foi feita nos dados da empresa.");
        }
    }
}
