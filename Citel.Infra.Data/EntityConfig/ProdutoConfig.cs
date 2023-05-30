using Citel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Infra.Data.EntityConfig
{
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Produto");

            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.NomeProduto)
                .IsRequired(true)
                .HasColumnName("NomeProduto")
                .HasColumnType("varchar(45)");

            entityTypeBuilder.Property(x => x.Preco)
                .IsRequired(true)
                .HasColumnName("Preco")
                .HasColumnType("decimal(5,2)");

            entityTypeBuilder.Property(x => x.Quantidade)
               .IsRequired(true)
               .HasColumnName("Quantidade")
               .HasColumnType("int");

            entityTypeBuilder.HasOne(x => x.Categoria)
              .WithMany(x => x.Produto)
              .HasForeignKey(x => x.IdCategoria)
              .OnDelete(DeleteBehavior.NoAction)
              .IsRequired(true);
        }
    }
}
