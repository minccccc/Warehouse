namespace Infrastructure.Http
{
    public interface IRetrieveProductsService
    {
        Task<string> GetProducts(string sourceUri, CancellationToken cancellationToken);
    }
}
