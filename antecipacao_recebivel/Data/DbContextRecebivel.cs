using antecipacao_recebivel.Models;
using Microsoft.EntityFrameworkCore;

namespace antecipacao_recebivel.Data
{
    public class DbContextRecebivel : DbContext
    {


        public DbContextRecebivel(DbContextOptions<DbContextRecebivel> options) 
            : base(options) { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<NotasFiscais> NotasFiscais { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>()
                .Property(e => e.faturamento)
                .HasColumnType("decimal(18,2)"); //  precisão e escala 
        }
        
    }
}
