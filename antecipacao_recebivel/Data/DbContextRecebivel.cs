using antecipacao_recebivel.Models;
using Microsoft.EntityFrameworkCore;

namespace antecipacao_recebivel.Data
{
    public class DbContextRecebivel : DbContext
    {
        public DbContextRecebivel(DbContextOptions<DbContextRecebivel> options) : base(options) { }

       
        public DbSet<Empresa> Empresas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
