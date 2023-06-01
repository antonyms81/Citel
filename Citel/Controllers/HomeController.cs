using Citel.Domain.Entities;
using Citel.Domain.Services;
using Citel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Citel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceCategoria _serviceCategoria;
        private readonly IServiceProduto _serviceProduto;

        public HomeController(ILogger<HomeController> logger, IServiceCategoria serviceCategoria, IServiceProduto serviceProduto)
        {
            _logger = logger;
            _serviceCategoria = serviceCategoria;
            _serviceProduto = serviceProduto;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Categoria(ProdutoCategoriaViewModel produtoCategoria)
        {
            var listaCategoria = await _serviceCategoria.BuscarTodos();

            produtoCategoria.Categoria = listaCategoria;

            return View(produtoCategoria);
        }


        [HttpPost]
        public async Task<IActionResult> Categoria(ProdutoCategoriaViewModel produtoCategoria, Categoria categopria)
        {
            categopria.NomeCategoria = produtoCategoria.NomeCategoria.ToString();

            try
            {
                var linhasAfetadas = await _serviceCategoria.Criar(Guid.NewGuid(), categopria);

                if (linhasAfetadas > 0)
                {
                    TempData["ViewDataAlert"] = "Categoria Cadastrado com sucesso :)";
                    return RedirectToAction(nameof(Categoria));
                }
            }
            catch (Exception ex)
            {
                TempData["ViewDataAlert"] = "Desculpe!!! Não foi possível realizar o cadastro.";
            }

            return View();
        }

       

        public async Task<IActionResult> EditarCategoria(Guid id, ProdutoCategoriaViewModel produtoCategoria)
        {
            Produto produto = new Produto();

            try
            {
                if (id != Guid.Empty)
                {
                    var _categoria = await _serviceCategoria.BuscarPorId(id);
                    produtoCategoria.NomeCategoria = _categoria.NomeCategoria;
                    produtoCategoria.IdCategoria = _categoria.Id;
                   
                    return View(produtoCategoria);
                }
            }
            catch (Exception ex)
            {
                TempData["ViewDataAlert"] = "Desculpe!!! Não foi possível editar categoria, entrar em contato com a Equipe Técnica.";
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> EditarCategoria(ProdutoCategoriaViewModel produtoCategoria, Categoria categoria)
        {
            categoria.Id = produtoCategoria.IdCategoria;
            categoria.NomeCategoria = categoria.NomeCategoria;
           
            try
            {
                var linhasAfetadas = await _serviceCategoria.Atualizar(categoria.Id, categoria);

                if (linhasAfetadas > 0)
                {
                    TempData["ViewDataAlert"] = "Categoria Alterado com sucesso :)";
                    return RedirectToAction(nameof(Categoria));
                }
            }
            catch (Exception ex)
            {
                TempData["ViewDataAlert"] = "Desculpe!!! Não foi possível realizar a auteração da Categoria.";
            }
            return View();
        }

        public async Task<IActionResult> DeletarCategoria(Guid id, ProdutoCategoriaViewModel produtoCategoria)
        {
            if (id != Guid.Empty)
            {
                var result = await _serviceCategoria.BuscarPorId(id);

                produtoCategoria.IdCategoria = result.Id;
                produtoCategoria.NomeCategoria = result.NomeCategoria;

                if (result != null)
                {
                    return View(produtoCategoria);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeletarCategoria(ProdutoCategoriaViewModel produtoCategoria)
        {
            var produtos = await _serviceProduto.BuscarTodos();
            var _produto = produtos.Where(x => x.IdCategoria == produtoCategoria.IdCategoria);

            try
            {
                foreach (var item in _produto)
                {
                    var resul = await _serviceProduto.Excluir(item.Id);
                }
               
                var linhasAfetadas = await _serviceCategoria.Excluir(produtoCategoria.IdCategoria);

                if (linhasAfetadas > 0)
                {
                    TempData["ViewDataAlert"] = "Categoria Excluido com sucesso :)";
                    return RedirectToAction(nameof(Categoria));
                }
            }
            catch (Exception ex)
            {
                TempData["ViewDataAlert"] = "Desculpe!!! Não foi possível Excluir Produto.";
            }
            return View();

        }
        public async Task<IActionResult> Produto(ProdutoCategoriaViewModel produtoCategoria)
        {
            var listaProduto = await _serviceProduto.BuscarTodos();
            var listaCategoria = await _serviceCategoria.BuscarTodos();

            produtoCategoria.Categoria = listaCategoria;
            produtoCategoria.Produto = listaProduto;

            return View(produtoCategoria);
        }

        [HttpPost]
        public async Task<IActionResult> Produto(ProdutoCategoriaViewModel produtoCategoria, Produto produto)
        {
            produto.NomeProduto = produtoCategoria.NomeProduto.ToString();
            produto.Preco = produtoCategoria.Preco;
            produto.Quantidade = produtoCategoria.Quantidade;
            produto.IdCategoria = produtoCategoria.IdCategoria;

            try
            {
                var linhasAfetadas = await _serviceProduto.CriarProduto(Guid.NewGuid(), produto);

                if (linhasAfetadas > 0)
                {
                    TempData["ViewDataAlert"] = "Produto Cadastrado com sucesso :)";
                    return RedirectToAction(nameof(Produto));
                }
            }
            catch (Exception ex)
            {
                TempData["ViewDataAlert"] = "Desculpe!!! Não foi possível realizar o cadastro.";
            }

            return View();
        }

        public async Task<IActionResult> EditarProduto(Guid id, ProdutoCategoriaViewModel produtoCategoria)
        {
            Produto produto = new Produto();
           
            try
            {
                if (id != Guid.Empty)
                {
                    var _produto = await _serviceProduto.BuscarPorId(id);
                    var _categoria = await _serviceCategoria.BuscarTodos();

                    produtoCategoria.NomeProduto = _produto.NomeProduto.ToString();
                    produtoCategoria.Preco = _produto.Preco;    
                    produtoCategoria.Quantidade = _produto.Quantidade;
                    produtoCategoria.Categoria = _categoria;
                    produtoCategoria.IdProduto = _produto.Id;


                    return View(produtoCategoria);
                }
            }
            catch (Exception ex)
            {
                TempData["ViewDataAlert"] = "Desculpe!!! Não foi possível editar produto, entrar em contato com a Equipe Técnica.";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditarProduto(ProdutoCategoriaViewModel produtoCategoria, Produto produto)
        {
            produto.NomeProduto = produtoCategoria.NomeProduto.ToString();
            produto.Preco = produtoCategoria.Preco;
            produto.Quantidade = produtoCategoria.Quantidade;
            produto.IdCategoria = produtoCategoria.IdCategoria;
            produto.Id = produtoCategoria.IdProduto;

            try
            {
                var linhasAfetadas = await _serviceProduto.Atualizar(produto.Id, produto);

                if (linhasAfetadas > 0)
                {
                    TempData["ViewDataAlert"] = "Produto Alterado com sucesso :)";
                    return RedirectToAction(nameof(Produto));
                }
            }
            catch (Exception ex)
            {
                TempData["ViewDataAlert"] = "Desculpe!!! Não foi possível realizar a auteração do Produto.";
            }
            return View();
        }


        public async Task<IActionResult> DeletarProduto(Guid id, ProdutoCategoriaViewModel produtoCategoria)
        {
            if (id != Guid.Empty)
            {
                var result = await _serviceProduto.BuscarPorId(id);

                produtoCategoria.IdProduto = result.Id;
                produtoCategoria.NomeProduto = result.NomeProduto;

                if (result != null)
                {
                    return View(produtoCategoria);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeletarProduto(ProdutoCategoriaViewModel produtoCategoria)
        {

            try
            {
                var linhasAfetadas = await _serviceProduto.Excluir(produtoCategoria.IdProduto);

                if (linhasAfetadas > 0)
                {
                    TempData["ViewDataAlert"] = "Produto Excluido com sucesso :)";
                    return RedirectToAction(nameof(Produto));
                }
            }
            catch (Exception ex)
            {
                TempData["ViewDataAlert"] = "Desculpe!!! Não foi possível Excluir Produto.";
            }
            return View();

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}