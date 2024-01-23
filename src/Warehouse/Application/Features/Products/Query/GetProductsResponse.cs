using Domain.Models;

namespace Application.Features.Products.Query
{
    public class GetProductsResponse
    {
        public ProductsSummary Summary { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
