using Newtonsoft.Json;

namespace Infrastructure.Http
{
    public class RetrieveProductsService : IRetrieveProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RetrieveProductsService(
            IHttpClientFactory httpClientFactory)
        {
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
                    throw new Exception($"Failed to retrieve products from the source {response.StatusCode}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
