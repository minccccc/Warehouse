using Application.Configuration;
using Application.Features.Commands.SyncProducts;
using MediatR;
using Microsoft.Extensions.Options;
using Quartz;

namespace Application.Services.Background;

public class ProductsSyncService : IJob
{
    private readonly IMediator _mediator;
    private readonly MockyClientConfig _mockyClientConfig;

    public ProductsSyncService(
        IMediator mediator,
        IOptions<MockyClientConfig> mockyClientConfig)
    {
        _mediator = mediator;
        _mockyClientConfig = mockyClientConfig.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var uri = _mockyClientConfig.BaseUrl + _mockyClientConfig.ProductUrl;
        await _mediator.Send(new SyncProductsCommand(uri));
    }
}
