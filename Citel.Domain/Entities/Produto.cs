using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public Guid IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        [NotMapped]
        public int Ordem { get; set; }
    }
}
