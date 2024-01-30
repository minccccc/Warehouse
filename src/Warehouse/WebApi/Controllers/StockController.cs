using Application.Features.Queries.GetProducts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.Controllers;

public class StockController : BaseController
{
    public StockController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    { }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] GetFilteredProductsRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetProductsQuery>(request);

        var productsResult = await _mediator.Send(query, cancellationToken);

        var response = _mapper.Map<QueryProductsResponse>(productsResult);

        return Ok(response);
    }
}