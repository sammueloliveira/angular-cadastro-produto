using AutoMapper;
using CatalagoProduto.APIs.Models.DTOs;
using CatalogoProduto.Domain.Entities;
using CatalogoProduto.Domain.Enums;
using CatalogoProduto.Domain.Interfaces;
using CatalogoProduto.Domain.InterfaceServices;
using Microsoft.AspNetCore.Mvc;

namespace CatalagoProduto.APIs.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produto;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        public ProdutoController(IProduto produto, IProdutoService produtoService, IMapper mapper)
        {
            _produto = produto;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ProdutoDTO>), 200)]
        [ProducesResponseType(400)] 
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            try
            {
                var produtos = await _produto.List();

                var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

                return Ok(produtosDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter produtos: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um produto específico pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>O produto correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProdutoDTO), 200)]
        [ProducesResponseType(404)] 
        [ProducesResponseType(400)] 
        public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
        {
            var produto = await _produto.GetEntityById(id);

            if (produto == null)
            {
                return NotFound("Produto cadastrado não encontrado. Verifique o ID e tente novamente.");
            }

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produtoDto">Os dados do produto a serem criados.</param>
        /// <returns>O produto criado.</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProdutoDTO), 201)]
        [ProducesResponseType(400)] 
        public async Task<ActionResult<ProdutoDTO>> PostProduto(ProdutoDTO produtoDto)
        {
            try
            {
                if (!Enum.IsDefined(typeof(TipoProdutoEnum), produtoDto.Tipo))
                {
                    return BadRequest("Tipo inválido. Deve ser 0 (Organico) ou 1 (NaoOrganico).");
                }

                var produto = _mapper.Map<Produto>(produtoDto);

                await _produtoService.AddProduto(produto);

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar o produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="produtoDto">Os dados do produto atualizados.</param>
        /// <returns>No content se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)] 
        [ProducesResponseType(404)] 
        public async Task<ActionResult> UpdateProduto(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.Id)
            {
                return BadRequest("O ID do produto na URL não corresponde ao ID do produto nos dados enviados.");
            }

            if (!Enum.IsDefined(typeof(TipoProdutoEnum), produtoDto.Tipo))
            {
                return BadRequest("Tipo inválido. Deve ser 0 (Organico) ou 1 (NaoOrganico).");
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            try
            {
                await _produtoService.UpdateProduto(produto);

                return Ok(produto);
            }
            catch (Exception)
            {
                return NotFound("Produto não encontrado!");
            }
        }

        /// <summary>
        /// Deleta um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser deletado.</param>
        /// <returns>No content se a deleção for bem-sucedida.</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)] 
        public async Task<ActionResult> DeleteProduto(int id)
        {
            var produto = await _produto.GetEntityById(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado. Verifique o ID e tente novamente!");
            }

            await _produto.Delete(produto);

            return Ok("Produto deletado com sucesso!");
        }

    }
}

