using MediatR;

namespace Application.Features.Products.Query
{
    public record GetProductsQuery(
        double? MinPrice,
        double? MaxPrice,
        List<string> Highlights,
        string Size) : IRequest<GetProductsResponse>;
}
