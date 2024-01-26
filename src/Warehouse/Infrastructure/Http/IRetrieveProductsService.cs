namespace Infrastructure.Http;

public interface IRetrieveProductsService
{
    Task<T> GetProducts<T>(string sourceUri, CancellationToken cancellationToken);
}