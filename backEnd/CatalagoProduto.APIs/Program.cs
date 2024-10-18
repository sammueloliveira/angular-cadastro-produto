using CatalagoProduto.APIs.Models;
using CatalagoProduto.APIs.Models.Mapping;
using CatalogoProduto.HelpConfig.HelpStartup;
using CatalogoProduto.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new DateOnlyBrazilianConverter());
    });


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Contexto>(options =>
           options.UseInMemoryDatabase("CatalogoProdutoDB"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CatalogoProduto.APIs",
        Version = "v1",
    });

   
    c.MapType<bool>(() => new OpenApiSchema
    {
        Type = "boolean",
        Default = new OpenApiBoolean(false)
    });

    c.SchemaFilter<SwaggerSchemaFilter>();

    var xmlFile = "CatalogoProduto.APIs.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

HelpStartup.ConfigureScoped(builder.Services);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        builder => builder.WithOrigins("http://localhost:4200") 
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
