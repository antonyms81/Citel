using Citel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Domain.Repositories
{
    public interface IBase<E> where E : class
    {
        Task<int> Criar(E entidade);
        Task<int> Atualizar(E entidade);
        Task<int> Excluir(Guid id);
        Task<List<E>> BuscarTodos();
        Task<E> BuscarPeloId(Guid id);
    }
}
