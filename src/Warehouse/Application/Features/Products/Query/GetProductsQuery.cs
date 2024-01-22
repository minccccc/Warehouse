using MediatR;

namespace Application.Features.Products.Query
{
    public record GetProductsQuery(string filter) : IRequest<string>;
}
