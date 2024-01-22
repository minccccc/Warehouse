using Application.Features.Products.Synchronize;
using MediatR;
using Microsoft.Extensions.Options;
using WebApi.Configuration;

namespace WebApi.Background
{
    public class ProductsSyncService : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsSyncService> _logger;
        private readonly ProductsSourceConfig _productsSourceConfig;

        public ProductsSyncService(
            IMediator mediator,
            ILogger<ProductsSyncService> logger,
            IOptions<ProductsSourceConfig> productsSourcs)
        {
            _logger = logger;
            _productsSourceConfig = productsSourcs.Value;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Retrieve information from the source...");

                    await _mediator.Send(new SyncProductsCommand(_productsSourceConfig.Uri));

                    await Task.Delay(TimeSpan.FromSeconds(_productsSourceConfig.RefreshTime), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve information from the source: {Message}", ex.Message);
            }
        }
    }
}