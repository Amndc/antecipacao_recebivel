using antecipacao_recebivel.Controllers;
using antecipacao_recebivel.Data;
using antecipacao_recebivel.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
             decimal prazoEmMeses = Math.Round((prazo / 30m), 2);
             
             decimal desagio = Math.Round((valorNF / (decimal)Math.Pow((1 + (double)taxa), (double)prazoEmMeses)), 0);
             decimal valorLiquido = desagio; // O correto é este valor
             
             return valorLiquido;
        }

        //calculo fixo de limite de acordo com o faturemento
        public decimal CalcularLimiteAntecipacao(string faturamento, int ramo)
        {
            faturamento = faturamento.Replace("R$", "").Trim().Replace(".", "");

            bool conversaoSucesso = decimal.TryParse(faturamento, NumberStyles.Number, new CultureInfo("pt-BR"), out decimal valorFaturamento);

            if (!conversaoSucesso)
            {
                throw new ArgumentException("O faturamento informado não está em um formato válido.");
            }

            decimal porcentagem = valorFaturamento switch
            {
                >= 100001m when ramo == 1 => 0.65m,
                >= 100001m when ramo == 2 => 0.60m,
                >= 50001m when ramo == 1 => 0.60m,
                >= 50001m when ramo == 2 => 0.55m,
                >= 10000m and < 50001m => 0.50m,
                _ => 0m
            };

            // Cálculo do limite de antecipação
            return valorFaturamento * porcentagem;
        }



    }
}
