using antecipacao_recebivel.Controllers;
using antecipacao_recebivel.Data;
using antecipacao_recebivel.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace antecipacao_recebivel.DataAccess
{
    public class NfRepository
    {
        private readonly DbContextRecebivel _DBrecebivel;
       

        public NfRepository(DbContextRecebivel DBrecebivel)        {
            _DBrecebivel = DBrecebivel;
         
        }

        public void adicionarNF(NotasFiscais notafiscal, string cnpj)
        {
            notafiscal.idempresa = _DBrecebivel.Empresas
                                                .Where(e => e.cnpj == cnpj)
                                                .Select(e => e.id).FirstOrDefault();
            _DBrecebivel.NotasFiscais.Add(notafiscal);
            _DBrecebivel.SaveChanges();
        }

        public List<NotasFiscais> listaNF(string cnpj)
        {
            var empresa = _DBrecebivel.Empresas.FirstOrDefault(e => e.cnpj == cnpj);
            if (empresa == null)
                return new List<NotasFiscais>();

            var notas = _DBrecebivel.NotasFiscais    .Where(n => n.idempresa == empresa.id)    .ToList();

            return notas;
        }

       
        public bool existeNF(string numero, string cnpj)
        {
            if (_DBrecebivel.NotasFiscais.Any(n => n.numero == numero && n.idempresa == _DBrecebivel.Empresas 
                                                                                        .Where(e => e.cnpj == cnpj)
                                                                                        .Select(e => e.id).FirstOrDefault()))
                return true;

            return false;
        }
        
        public decimal CalculaDesagio(decimal valorNF, DateTime dataVencimento, DateTime dataAtual)
        {

          
             int prazo = (dataVencimento - dataAtual).Days;
             decimal taxa = 0.0465m; // 4.65% ao mês
             decimal prazoEmMeses = prazo / 30m;
             
             decimal desagio = valorNF / (decimal)Math.Pow((1 + (double)taxa), (double)prazoEmMeses);
             decimal valorLiquido = desagio; // O correto é este valor
             
             return valorLiquido;
        }

        //calculo fixo de limite de acordo com o faturemento
        public decimal CalcularLimiteAntecipacao(decimal faturamento, int ramo)
        {
            decimal porcentagem = (faturamento, ramo) switch
            {
                // > R$ 100.000
                ( >= 100001, 1) => 0.65m, // Produtos
                ( >= 100001, 2) => 0.60m, // Serviços

                //  R$ 50.001 e R$ 100.000
                ( >= 50001, 1) => 0.60m, 
                ( >= 50001, 2) => 0.55m, 

                //  R$ 10.000 e R$ 50.000 
                ( >= 10000, _) => 0.50m,

                // nenhum critério, retorna 0
                _ => 0m
            };


            if (porcentagem == 0m)
            {
                return 0m;
            }

            return faturamento * porcentagem;
        }

    

    }
}
