using Application.Models.DTOs;
using MediatR;

namespace Application.Features.Queries.GetProducts;

public record GetProductsQuery(
    decimal? MinPrice,
    decimal? MaxPrice,
    List<string> Highlights,
    string Size) : IRequest<GetProductsDto>;