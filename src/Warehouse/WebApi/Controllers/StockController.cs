using Application.Features.Products.Query;
using Application.Features.Products.Synchronize;
using Infrastructure.Http;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Configuration;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly ProductsSourceConfig _productsSourceConfig;
        private readonly IRetrieveProductsService _retrieveProductsService;
        private readonly IMediator _mediator;

        public StockController(
            ILogger<StockController> logger,
            IOptions<ProductsSourceConfig> productsSourcs,
            IRetrieveProductsService retrieveProductsService,
            IMediator mediator)
        {
            _logger = logger;
            _productsSourceConfig = productsSourcs.Value;
            _retrieveProductsService = retrieveProductsService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string input, CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetProductsQuery(input));

            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
