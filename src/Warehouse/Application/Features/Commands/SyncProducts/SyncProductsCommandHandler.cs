using Application.Common.Constants;
using Application.Extensions;
using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Features.Commands.SyncProducts;

public class SyncProductsCommandHandler : IRequestHandler<SyncProductsCommand>
{
    private readonly IMockyHttpClientService _mockyHttpClientService;
    private readonly ICacheProvider _cacheProvider;

    public SyncProductsCommandHandler(
        IMockyHttpClientService mockyHttpClientService,
        ICacheProvider cacheProvider)
    {
        _mockyHttpClientService = mockyHttpClientService;
        _cacheProvider = cacheProvider;
    }

    public async Task Handle(SyncProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await _mockyHttpClientService.GetProducts<List<Product>>(request.sourceUri, cancellationToken);

        var summary = new ProductsSummary()
        {
            MinPrice = products.Min(p => p.Price),
            MaxPrice = products.Max(p => p.Price),
            Sizes = products.SelectMany(p => p.Sizes).Distinct().ToList(),
            Highlights = products.CollectCommonWords()
        };

        _cacheProvider.Set(AppConstants.ProductsCacheKey, products);
        _cacheProvider.Set(AppConstants.ProductsSummaryCacheKey, summary);
    }
}