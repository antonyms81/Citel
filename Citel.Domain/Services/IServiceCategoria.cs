using Citel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Domain.Services
{
    public interface IServiceCategoria
    {
        Task<int> Criar(Guid id, Categoria categoria);
        Task<int> Atualizar(Guid id, Categoria categoria);
        Task<int> AtualizarItem(Guid id, Produto produto);
        Task<int> ExcluirItem(Guid id, Produto produto);
        Task<int> Excluir(Guid id);
        Task<List<Categoria>> BuscarTodos();
        Task<Categoria> BuscarPorId(Guid id);
        Task<List<Produto>> BuscarItem();
    }
}
