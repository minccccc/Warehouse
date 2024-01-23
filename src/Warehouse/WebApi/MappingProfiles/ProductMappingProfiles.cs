using Application.Features.Products.Query;
using AutoMapper;
using Domain.Models;
using WebApi.DTOs;

namespace WebApi.MappingProfiles
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductsSummary, ProductsSummaryDto>();
            CreateMap<GetProductsResponse, QueryProductsResponseDto>();
        }
    }
}
