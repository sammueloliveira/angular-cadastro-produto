using CatalogoProduto.Domain.Entities;

namespace CatalogoProduto.Domain.InterfaceServices
{
    public interface IProdutoService
    {
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);
    }
}
