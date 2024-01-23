using Domain.Models;

namespace WebApi.DTOs
{
    public class QueryProductsResponseDto
    {
        public ProductsSummaryDto Summary { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
