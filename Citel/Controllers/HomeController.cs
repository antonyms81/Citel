using Citel.Domain.Entities;
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
        private readonly IServiceCategoria _service;
        private readonly IServiceProduto _serviceProduto;

        public HomeController(ILogger<HomeController> logger, IServiceCategoria service, IServiceProduto serviceProduto)
        {
            _logger = logger;
            _service = service;
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

        public async Task<IActionResult> Produto(ProdutoCategoriaViewModel produtoCategoria)
        {
            ListaCategoria listaCategoria = new ListaCategoria();

            var result = await _service.BuscarTodos();

            produtoCategoria.Categoria = result;

            return View(produtoCategoria);
        }


        [HttpPost]
        public async Task<IActionResult> Produto(ProdutoCategoriaViewModel produtoCategoria, Produto produto)
        {
            produto.NomeProduto = produtoCategoria.NomeProduto.ToString();
            produto.Preco = produtoCategoria.Preco;
            produto.Quantidade = produtoCategoria.Quantidade;
            produto.IdCategoria = produtoCategoria.IdCategoria;

            var linhasAfetadas = await _serviceProduto.CriarProduto(Guid.NewGuid(), produto);

            if (linhasAfetadas > 0)
            {
                TempData["ViewDataAlert"] = "Produto Cadastrado com sucesso :)";
                return RedirectToAction(nameof(Produto));
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