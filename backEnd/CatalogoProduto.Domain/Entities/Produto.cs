using CatalogoProduto.Domain.Enums;

namespace CatalogoProduto.Domain.Entities
{
    public class Produto
    {  
        public int Id { get; set; }
        public string Nome { get; set; } 
        public decimal PrecoDeVenda { get; set; }
        public string Descricao { get; set; } 
        public int Quantidade { get; set; }
        public TipoProdutoEnum Tipo { get; set; }
        public DateOnly DataDeCadastro { get; set; }
       

    }
}
