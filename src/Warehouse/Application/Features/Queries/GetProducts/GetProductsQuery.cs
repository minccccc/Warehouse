using Application.Models;
using MediatR;

namespace Application.Features.Queries.GetProducts;

public record GetProductsQuery(
    double? MinPrice,
    double? MaxPrice,
    List<string> Highlights,
    string Size) : IRequest<GetProductsDto>;