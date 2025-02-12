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

        public NfRepository(DbContextRecebivel DBrecebivel)
        {
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

            // 4,65%  mês
            decimal taxa = 0.0465m;
        
            decimal prazoEmMeses = prazo / 30m;
     
            double desagio = (double)valorNF / Math.Pow((1 + (double)taxa), (double)prazoEmMeses);
           
            decimal valorLiquido = valorNF - (decimal)desagio;

            return valorLiquido;
        }

    }
}
