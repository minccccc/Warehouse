using Application.Common.Constants;
using Application.Extensions;
using Application.Interfaces;
using Application.Models.DTOs;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Features.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsDto>
{
    private readonly IMapper _mapper;
    private readonly ICacheProvider _cacheProvider;

    public GetProductsQueryHandler(ICacheProvider cacheProvider, IMapper mapper)
    {
        _cacheProvider = cacheProvider;
        _mapper = mapper;
    }

    public async Task<GetProductsDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var cachedProducts = _cacheProvider.Get<List<Product>>(AppConstants.ProductsCacheKey);
        var summary = _cacheProvider.Get<ProductsSummary>(AppConstants.ProductsSummaryCacheKey);

        var result = _mapper.Map<GetProductsDto>(summary);
        result.Products = cachedProducts.Where(p =>
                (!request.MinPrice.HasValue || p.Price >= request.MinPrice)
                && (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice)
                && (string.IsNullOrEmpty(request.Size) || p.Sizes.Contains(request.Size, StringComparer.OrdinalIgnoreCase)))
            .Select(p => p.Highlight(request.Highlights));

        return await Task.FromResult(result);
    }
}