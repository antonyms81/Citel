using Citel.Domain.Entities;
using Citel.Domain.Services;
using Citel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Citel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceCategoria _service;
       
        public HomeController(ILogger<HomeController> logger, IServiceCategoria service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Categoria()
        {
           
            return View();
        }

        public async Task<IActionResult> Produto()
        {
            ListaCategoria listaCategoria = new ListaCategoria();

            var result = await _service.BuscarTodos();

            listaCategoria.Categoria = result;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Produto(Categoria categoria)
        {
            if (Request.Form["TipoAcao"] == "AdicionarProduto")
                categoria.Produto.Add(new Produto { Id = Guid.NewGuid(), Ordem = categoria.Produto.Count });

            if (Request.Form["TipoAcao"] == "CadastrarProduto")
            {
                var linhasAfetadas = await _service.Criar(Guid.NewGuid(), categoria);
                if (linhasAfetadas > 0)
                {
                    TempData["ViewDataAlert"] = "Produto Cadastrado com sucesso :)";
                    return RedirectToAction(nameof(Produto));
                }


            }
            return View(categoria);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}