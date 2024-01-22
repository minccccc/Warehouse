using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using WebApi.Configuration;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly ProductsSourceConfig _productsSourceConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public StockController(
            ILogger<StockController> logger,
            IOptions<ProductsSourceConfig> productsSourcs,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _productsSourceConfig = productsSourcs.Value;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(_productsSourceConfig.Uri, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);
                return Ok(stringResult);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
