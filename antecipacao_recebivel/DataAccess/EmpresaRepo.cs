using antecipacao_recebivel.Models;

namespace antecipacao_recebivel.DataAccess
{
    public class EmpresaRepo
    {
        private readonly MeuDbContext _context;

        public EmpresaRepo(MeuDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            _context.SaveChanges();
        }

        public Empresa ObterPorCNPJ(string cnpj)
        {
            return _context.Empresas.FirstOrDefault(e => e.CNPJ == cnpj);
        }

        public bool existsEmpresa (string cnpj)
        {
            return _context.Empresas.Any(e => e.CNPJ == cnpj);
        }
    }
}
