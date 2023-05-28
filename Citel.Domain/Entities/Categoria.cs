using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Domain.Entities
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string NomeCategoria { get; set; }

        public virtual List<Produto> Produto { get; set; } = new();
    }
}
