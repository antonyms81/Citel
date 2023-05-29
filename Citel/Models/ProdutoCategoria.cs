using Citel.Domain.Entities;

namespace Citel.Models
{
    public class ProdutoCategoriaViewModel
    {
        public Guid IdProduto { get; set; }
        public Guid IdCategoria { get; set; }
        public string NomeProduto { get; set; } 
        public string NomeCategoria { get; set; } 
        public decimal Preco { get; set; } 
        public int Quantidade { get; set; }
        public List<Categoria> Categoria { get; set; } = new();
        public List<Produto> Produto { get; set; } = new();


    }
}