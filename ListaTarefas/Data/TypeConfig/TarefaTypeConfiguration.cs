using ListaTarefas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListaTarefas.Data.TypeConfig
{
    public class TarefaTypeConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("tarefas");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnType("int")
                .HasColumnName("id")
                .IsRequired();

            builder
                .Property(x => x.Titulo)
                .HasColumnType("varchar(60)")
                .HasColumnName("titulo")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnType("varchar(300)")
                .HasColumnName("descricao")
                .IsRequired();

            builder
                .Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            builder
                .Property(x => x.DataDeCriacao)
                .HasColumnType ("datetime")
                .HasColumnName("data_criacao")
                .IsRequired();
            
        }
    }
}
