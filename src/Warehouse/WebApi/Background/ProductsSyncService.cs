using Application.Features.Products.Synchronize;
using MediatR;
using Microsoft.Extensions.Options;
using WebApi.Configuration;

namespace WebApi.Background
{
    public class ProductsSyncService : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly ProductsSourceConfig _productsSourceConfig;

        public ProductsSyncService(
            IMediator mediator,
            IOptions<ProductsSourceConfig> productsSourcs)
        {
            _productsSourceConfig = productsSourcs.Value;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _mediator.Send(new SyncProductsCommand(_productsSourceConfig.Uri));

                await Task.Delay(TimeSpan.FromSeconds(_productsSourceConfig.RefreshTime), stoppingToken);
            }
        }
    }
}