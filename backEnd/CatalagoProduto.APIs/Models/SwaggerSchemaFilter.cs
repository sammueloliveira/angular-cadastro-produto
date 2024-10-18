using CatalagoProduto.APIs.Models.DTOs;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CatalagoProduto.APIs.Models
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(ProdutoDTO))
            {
                if (schema.Properties.ContainsKey("dataDeCadastro"))
                {
                    var today = DateTime.Now.ToString("dd/MM/yyyy");
                    schema.Properties["dataDeCadastro"].Description = "Data de cadastro no formato dd/MM/yyyy.";
                    schema.Properties["dataDeCadastro"].Example = new OpenApiString(today);
                }
            }
        }


    }

}
