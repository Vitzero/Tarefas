using ListaTarefas.Data.TypeConfig;
using ListaTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TarefaTypeConfiguration).Assembly);
        }
    }
}
