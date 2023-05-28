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
    public class CategoriaConfig : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Categoria");

            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.NomeCategoria)
               .IsRequired()
               .HasColumnName("NomeCategoria")
               .HasColumnType("varchar(45)");
        }
    }
}
