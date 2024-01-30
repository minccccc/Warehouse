namespace Application.Interfaces;

public interface IMockyHttpClientService
{
    Task<T> GetProducts<T>(string sourceUri, CancellationToken cancellationToken);
}