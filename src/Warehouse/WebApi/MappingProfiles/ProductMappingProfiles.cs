using Application.Features.Queries.GetProducts;
using Application.Models;
using AutoMapper;
using Domain.Models;
using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.MappingProfiles;

public class ProductMappingProfiles : Profile
{
    public ProductMappingProfiles()
    {
        CreateMap<Product, ProductResponse>();
        CreateMap<ProductsSummary, ProductsSummaryResponse>();
        CreateMap<GetProductsDto, QueryProductsResponse>();
        CreateMap<GetFilteredProductsRequest, GetProductsQuery>();
        CreateMap<ProductsSummary, GetProductsDto>()
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src));
    }
}