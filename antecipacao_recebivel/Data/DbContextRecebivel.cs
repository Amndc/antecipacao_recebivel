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
        public DbSet<LimiteAntecipacao> LimiteAntecipacao { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>()
                .Property(e => e.faturamento)
                .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<NotasFiscais>()
                   .HasKey(nf => nf.idnotafiscal);

            modelBuilder.Entity<LimiteAntecipacao>()
           .ToTable("LimiteAntecipacao")
           .HasKey(l => l.id);

            modelBuilder.Entity<LimiteAntecipacao>()
                .Property(l => l.porcentagem)
                .HasColumnType("int");

            modelBuilder.Entity<LimiteAntecipacao>()
                .Property(l => l.faixaMin)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<LimiteAntecipacao>()
                .Property(l => l.faixaMax)
                .HasColumnType("decimal(18, 2)");
        }
        
    }
}
