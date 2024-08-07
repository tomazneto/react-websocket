using Microsoft.EntityFrameworkCore;
using ProvaConceitoSignalR.Model;
using System.Reflection;

namespace ProvaConceitoSignalR.Infra
{
    public class Contexto(DbContextOptions<Contexto> options) : DbContext(options)
    {
        public DbSet<Requisicao> Requisicoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Requisicao>(entity =>
            {
                entity.HasKey("Id");
                entity.Property(e => e.ano);
                entity.Property(e => e.orderid);
            });


            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}