using CatalogoProduto.Domain.Interfaces;
using CatalogoProduto.Domain.InterfaceServices;
using CatalogoProduto.Domain.Services;
using CatalogoProduto.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogoProduto.HelpConfig.HelpStartup
{
     public static class HelpStartup
     {
        public static void ConfigureScoped(IServiceCollection services)
        {
            // INTERFACE REPOSITORIO
            services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>));
            services.AddScoped<IProduto, ProdutoRepository>();

            // SERVICO DOMINIO
            services.AddScoped<IProdutoService, ProdutoService>();
        }
     }
}
