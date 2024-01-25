using Application.Features.Products.Query;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public StockController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductsQuery query, CancellationToken cancellationToken)
        {
            var productsResult = await _mediator.Send(query, cancellationToken);

            var response = _mapper.Map<QueryProductsResponseDto>(productsResult);

            return Ok(response);
        }
    }
}
