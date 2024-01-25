using Application.Configuration;
using Application.Features.Products.Synchronize;
using MediatR;
using Microsoft.Extensions.Options;
using Quartz;

namespace Application.Background
{
    public class ProductsSyncService : IJob
    {
        private readonly IMediator _mediator;

        private readonly ProductsSourceConfig _productsSourceConfig;

        public ProductsSyncService(
            IMediator mediator,
            IOptions<ProductsSourceConfig> productsSourcs)
        {
            _mediator = mediator;
            _productsSourceConfig = productsSourcs.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await _mediator.Send(new SyncProductsCommand(_productsSourceConfig.Uri));
            }
            catch { }
        }
    }
}
