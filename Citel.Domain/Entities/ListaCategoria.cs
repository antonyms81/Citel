using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Domain.Entities
{
    public class ListaCategoria
    {
        public List<Categoria> Categoria { get; set; } = new();
    }
}
