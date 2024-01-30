using Application.Common;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Http;

public class MockyHttpClientService : IMockyHttpClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<MockyHttpClientService> _logger;

    public MockyHttpClientService(
        IHttpClientFactory httpClientFactory,
        ILogger<MockyHttpClientService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
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
        catch (Exception ex)
        {
            _logger.LogrRetrieveProductsFailure(ex.Message);
            throw;
        }
    }
}