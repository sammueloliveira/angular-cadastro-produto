using CatalogoProduto.Domain.Entities;
using CatalogoProduto.Domain.Interfaces;
using CatalogoProduto.Infra.Data;

namespace CatalogoProduto.Infra.Repositories
{
    public class ProdutoRepository : GenericRepository<Produto>, IProduto
    {
        private readonly Contexto _contexto;
        public ProdutoRepository(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }   
    }
}
