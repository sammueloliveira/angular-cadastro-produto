using CatalogoProduto.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CatalogoProduto.Infra.Data
{
    public class Contexto : IdentityDbContext
     {
        public Contexto(DbContextOptions<Contexto> options): base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
