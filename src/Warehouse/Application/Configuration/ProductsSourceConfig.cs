namespace Application.Configuration;

public class ProductsSourceConfig
{
    public required string Uri { get; set; }

    public required int RefreshTime { get; set; }
}
