using Application.Exceptions;
using Application.Extensions;
using Domain.Models;
using FluentValidation;
using Infrastructure.Cache;
using MediatR;

namespace Application.Features.Products.Query
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
    {
        private readonly ICacheProvider _cacheProvider;
        private IValidator<GetProductsQuery> _validator;

        public GetProductsHandler(ICacheProvider cacheProvider, IValidator<GetProductsQuery> validator)
        {
            _cacheProvider = cacheProvider;
            _validator = validator;
        }

        public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ModelValidationException(validationResult);
            }

            var cachedProducts = _cacheProvider.Get<List<Product>>(AppConstants.ProductsCacheKey);
            var summary = _cacheProvider.Get<ProductsSummary>(AppConstants.ProductsSummaryCacheKey);

            var filteredProducts = cachedProducts.Where(p =>
                    (!request.MinPrice.HasValue || p.Price >= request.MinPrice)
                    && (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice)
                    && (string.IsNullOrEmpty(request.Size) || p.Sizes.Contains(request.Size, StringComparer.OrdinalIgnoreCase)))
                .Select(p => p.Highlight(request.Highlights));

            return new GetProductsResponse()
            {
                Products = filteredProducts,
                Summary = summary
            };
        }

    }
}
