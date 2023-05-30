using Citel.Domain.Entities;
using Citel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citel.Domain.Services
{
    public class ServiceProduto : IServiceProduto
    {
        private readonly IBase<Produto> _repositorio;

        public ServiceProduto(IBase<Produto> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<int> CriarProduto(Guid id, Produto produto)
        {
            produto.Id = id;

            return await _repositorio.Criar(produto);
        }

        public async Task<int> Atualizar(Guid id, Produto produto)
        {
            produto.Id = id;
            return await _repositorio.Atualizar(produto);
        }

        public async Task<int> Excluir(Guid id)
        {
            return await _repositorio.Excluir(id);
        }

        public async Task<List<Produto>> BuscarTodos()
        {
            return await _repositorio.BuscarTodos();
        }

        public async Task<Produto> BuscarPorId(Guid id)
        {
            return await _repositorio.BuscarPeloId(id);
        }

    }
}
