using Domain.Models;

namespace Application.Models.DTOs;

public class GetProductsDto
{
    public ProductsSummary Summary { get; set; }

    public IEnumerable<Product> Products { get; set; }
}