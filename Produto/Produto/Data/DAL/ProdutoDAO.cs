using Microsoft.EntityFrameworkCore;
using Modelo.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produto.Data.DAL
{
    public class ProdutoDAO
    {
        private readonly IESContext _context;

        #region Construtor
        public ProdutoDAO(IESContext context)
        {
            _context = context;
        }
        #endregion

        #region ObterProdutos
        public async Task<List<ProdutoM>> ObterProdutos()
        {
            return await _context.Produtos.OrderBy(p => p.ProdutoID).ToListAsync();
        }
        #endregion

        #region ObterProdutosPorId
        public async Task<ProdutoM> ObterProdutoPorId(long id)
        {
            return await _context.Produtos.FindAsync(id);
        }
        #endregion

        #region GravarProduto
        public async Task<ProdutoM> GravarProduto(ProdutoM produto)
        {
            if (produto.ProdutoID == null)
            {
                _context.Produtos.Add(produto);
            }
            else
            {
                _context.Update(produto);
            }
            await _context.SaveChangesAsync();

            return produto;
        }
        #endregion

        #region EliminarProdutoPorId
        public async Task<ProdutoM> EliminarProdutoPorId(long id)
        {
            ProdutoM produto = await ObterProdutoPorId(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
        }
        #endregion


    }
}
