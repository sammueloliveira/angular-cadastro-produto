using AutoMapper;
using CatalagoProduto.APIs.Models.DTOs;
using CatalogoProduto.Domain.Entities;

namespace CatalagoProduto.APIs.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }

}
