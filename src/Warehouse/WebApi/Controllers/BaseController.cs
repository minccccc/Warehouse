using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;

    public BaseController(IMediator mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
}
