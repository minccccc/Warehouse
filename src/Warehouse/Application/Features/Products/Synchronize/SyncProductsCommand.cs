using MediatR;

namespace Application.Features.Products.Synchronize
{
    public record SyncProductsCommand(string sourceUri) : IRequest;
}
