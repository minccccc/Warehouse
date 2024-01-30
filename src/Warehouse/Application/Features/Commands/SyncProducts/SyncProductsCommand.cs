using MediatR;

namespace Application.Features.Commands.SyncProducts;

public record SyncProductsCommand(string sourceUri) : IRequest;