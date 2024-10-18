using CatalogoProduto.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace CatalagoProduto.APIs.Models.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
        public decimal PrecoDeVenda { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; } 

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Tipo é obrigatório.")]
        public TipoProdutoEnum Tipo { get; set; } 

        [JsonConverter(typeof(DateOnlyBrazilianConverter))]
        [Required(ErrorMessage = "Data de cadastro é obrigatória.")]
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Formato de data inválido. Use DD/MM/AAAA.")]
        public DateOnly DataDeCadastro { get; set; }



    }

}
