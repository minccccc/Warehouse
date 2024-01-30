using Application.Features.Queries.GetProducts;
using Application.Models.DTOs;
using AutoMapper;
using Domain.Models;
using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.MappingProfiles;

public class ProductMappingProfiles : Profile
{
    public ProductMappingProfiles()
    {
        MapFromDtoToResponse();
        MapFromRequestToQueriesOrCommands();
        MapFromDomainModelToDto();
    }

    private void MapFromDtoToResponse()
    {
        CreateMap<Product, ProductResponse>();
        CreateMap<ProductsSummary, ProductsSummaryResponse>();
        CreateMap<GetProductsDto, QueryProductsResponse>();
    }

    private void MapFromRequestToQueriesOrCommands()
    {
        CreateMap<GetFilteredProductsRequest, GetProductsQuery>();
    }

    private void MapFromDomainModelToDto()
    {
        CreateMap<ProductsSummary, GetProductsDto>()
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src));
    }
}