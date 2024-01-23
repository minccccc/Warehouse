using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Http
{
    public class RetrieveProductsService : IRetrieveProductsService
    {
        private readonly ILogger<RetrieveProductsService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public RetrieveProductsService(
            ILogger<RetrieveProductsService> logger, 
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetProducts<T>(string sourceUri, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(sourceUri, cancellationToken);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

                    return JsonConvert.DeserializeObject<T>(stringResult);
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve products from the source");
                throw;
            }
        }
    }
}
