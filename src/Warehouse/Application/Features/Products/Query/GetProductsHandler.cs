using Infrastructure.Cache;
using MediatR;

namespace Application.Features.Products.Query
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, string>
    {
        private readonly ICacheProvider _cacheProvider;

        public GetProductsHandler(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public Task<string> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var filteredProducts = _cacheProvider.Get<string>(AppConstants.ProductsCacheKey);

            //TODO --> do filtering and common info preparation

            return Task.FromResult(filteredProducts);
        }
    }
}
