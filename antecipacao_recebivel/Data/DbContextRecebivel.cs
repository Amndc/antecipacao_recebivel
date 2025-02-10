using antecipacao_recebivel.Models;
using Microsoft.EntityFrameworkCore;

namespace antecipacao_recebivel.Data
{
    public class DbContextRecebivel : DbContext
    {

        public DbSet<Empresa> Empresas { get; set; }

        public DbContextRecebivel(DbContextOptions<DbContextRecebivel> options) : base(options) { }  
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestDb");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
