using Citel.Domain.Entities;
using Citel.Infra.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Infra.Data.Context
{
    public class CitelContext : DbContext
    {
        public CitelContext()
        {

        }

        public CitelContext(DbContextOptions<CitelContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=DbCitel;Integrated Security=true;encrypt=false;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>(new CategoriaConfig().Configure);
            modelBuilder.Entity<Produto>(new ProdutoConfig().Configure);
        }
    }
}
