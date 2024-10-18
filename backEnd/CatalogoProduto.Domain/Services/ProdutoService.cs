using CatalogoProduto.Domain.Entities;
using CatalogoProduto.Domain.Interfaces;
using CatalogoProduto.Domain.InterfaceServices;

namespace CatalogoProduto.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProduto _produto;
        public ProdutoService(IProduto produto)
        {
            _produto = produto;
        }
        public async Task AddProduto(Produto produto)
        {
            await _produto.Add(produto);
        }

        public async Task UpdateProduto(Produto produto)
        {
            var existeProduto = await _produto.GetEntityById(produto.Id);

            if(existeProduto != null)
            {
                existeProduto.Nome = produto.Nome;
                existeProduto.PrecoDeVenda = produto.PrecoDeVenda;
                existeProduto.Descricao = produto.Descricao;
                existeProduto.Quantidade = produto.Quantidade;
                existeProduto.Tipo = produto.Tipo;
                existeProduto.DataDeCadastro = produto.DataDeCadastro;

                await _produto.Update(existeProduto);
            }
            else
            {
                throw new InvalidOperationException($"Produto com ID {produto.Id} não encontrado.");
            }
        }
    }
}


