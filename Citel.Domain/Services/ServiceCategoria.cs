using Citel.Domain.Entities;
using Citel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Domain.Services
{
    public class ServiceCategoria : IServiceCategoria
    {
        private readonly IBase<Categoria> _repositorio;

        public ServiceCategoria(IBase<Categoria> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<int> Criar(Guid id, Categoria categoria)
        {
            categoria.Id = id;

            if (categoria.Produto != null)
            {
                categoria.Produto.ForEach(x => x.IdCategoria = id);
            }

            return await _repositorio.Criar(categoria);
        }

        public async Task<int> Atualizar(Guid id, Categoria categoria)
        {
            categoria.Id = id;
            return await _repositorio.Atualizar(categoria);
        }

        public async Task<int> AtualizarItem(Guid id, Produto produto)
        {
            produto.Id = id;
            return await _repositorio.AtualizarItem(produto);
        }

        public async Task<int> Excluir(Guid id)
        {
            return await _repositorio.Excluir(id);
        }

        public async Task<int> ExcluirItem(Guid id, Produto produto)
        {
            return await _repositorio.ExcluirItem(produto);
        }

        public async Task<List<Categoria>> BuscarTodos()
        {
            return await _repositorio.BuscarTodos();
        }

        public async Task<Categoria> BuscarPorId(Guid id)
        {
            return await _repositorio.BuscarPeloId(id);
        }

        public async Task<List<Produto>> BuscarItem()
        {
            return await _repositorio.BuscarItem();
        }
    }
}
