using Infrastructure.Cache;
using Infrastructure.Http;
using MediatR;

namespace Application.Features.Products.Synchronize
{
    public class SyncProductsHandler : IRequestHandler<SyncProductsCommand>
    {
        private readonly IRetrieveProductsService _retrieveProductsService;
        private readonly ICacheProvider _cacheProvider;

        public SyncProductsHandler(
            IRetrieveProductsService retrieveProductsService, 
            ICacheProvider cacheProvider)
        {
            _retrieveProductsService = retrieveProductsService;
            _cacheProvider = cacheProvider;
        }

        public async Task Handle(SyncProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await _retrieveProductsService.GetProducts(request.sourceUri, cancellationToken);

            _cacheProvider.Set(AppConstants.ProductsCacheKey, products);
        }
    }
}
