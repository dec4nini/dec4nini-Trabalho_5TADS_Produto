using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastro;
using Produto.Data;
using Produto.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produto.Areas.Cadastros.Controllers
{
    [Area(nameof(Cadastros))]
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly IESContext _context;
        private readonly ProdutoDAO _produtoDAO;

        #region Construtor
        public ProdutoController(IESContext context)
        {
            _context = context;
            _produtoDAO = new ProdutoDAO(context);
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _produtoDAO.ObterProdutos()); ;
        }
        #endregion

        #region Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Descricao, Distribuidora")] ProdutoM produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _produtoDAO.GravarProduto(produto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(ex.Message, "Falha ao inserir");
            }
            return View(produto);
        }
        #endregion

        #region Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterVisaoProdutoPorId(id);
        }
        #endregion

        #region Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ProdutoID, Nome, Descricao, Distribuidora")]
        ProdutoM produto)
        {
            if (id != produto.ProdutoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _produtoDAO.GravarProduto(produto);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(ex.Message, "Falha ao atualizar");
                }
            }


            return View(produto);
        }
        #endregion

        #region Details - GET
        public async Task<IActionResult> Details(long? id)
        {

            return await ObterVisaoProdutoPorId(id);
        }
        #endregion

        #region Delete - GET
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoProdutoPorId(id);
        }
        #endregion

        #region Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {

            var prod = await _produtoDAO.EliminarProdutoPorId((long)id);

            TempData["Message"] = "Produto " + prod.Nome + " foi removido!";


            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ObterVisaoProdutoPorId
        private async Task<IActionResult> ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtoDAO.ObterProdutoPorId((long)id);

            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }
        #endregion
    }
}
