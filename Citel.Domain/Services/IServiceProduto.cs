using Citel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Domain.Services
{
    public interface IServiceProduto
    {
        Task<int> CriarProduto(Guid id, Produto produto);
        Task<int> Atualizar(Guid id, Produto produto);
        Task<int> Excluir(Guid id);
        Task<List<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(Guid id);
    }
}
