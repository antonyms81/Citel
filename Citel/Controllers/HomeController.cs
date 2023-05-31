﻿using Citel.Domain.Entities;
using Citel.Domain.Services;
using Citel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

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


        public IActionResult Categoria()
        {
            return View();
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

        public async Task<IActionResult> EditarProduto(Guid id)
        {
           
            
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}