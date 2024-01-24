using Application.Extensions;
using Domain.Models;
using Infrastructure.Cache;
using Infrastructure.Http;
using Infrastructure.Logger;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Products.Synchronize
{
    public class SyncProductsHandler : IRequestHandler<SyncProductsCommand>
    {
        private readonly ILogger<SyncProductsHandler> _logger;
        private readonly IRetrieveProductsService _retrieveProductsService;
        private readonly ICacheProvider _cacheProvider;

        public SyncProductsHandler(
            ILogger<SyncProductsHandler> logger,
            IRetrieveProductsService retrieveProductsService,
            ICacheProvider cacheProvider)
        {
            _logger = logger;
            _retrieveProductsService = retrieveProductsService;
            _cacheProvider = cacheProvider;
        }

        public async Task Handle(SyncProductsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogRetievingProductstInfoMessage();

            try
            {
                var products = await _retrieveProductsService.GetProducts<List<Product>>(request.sourceUri, cancellationToken);

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
            catch (Exception ex)
            {
                _logger.LogFailedToRetrieveProductsExceptionMessage(ex.Message);
            }
        }
    }
}
