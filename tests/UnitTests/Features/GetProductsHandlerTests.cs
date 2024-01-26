using Application.Common.Constants;
using Application.Features.Queries.GetProducts;
using Application.Models;
using AutoMapper;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Cache;
using Microsoft.OpenApi.Any;
using Moq;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Features;

public class GetProductsHandlerTests
{
    [Fact]
    public async Task Should_ReturnAllProducts()
    {
        var query = new GetProductsQuery(null, null, new List<string>(), null);
        var result = await SendToHandler(query);

        result.Should().BeOfType<GetProductsDto>();
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, 100.00)]
    [InlineData(0.00, null)]
    [InlineData(20.00, 35.00)]
    [InlineData(0.00, 100.00)]
    public async Task Should_FilterByPrice(double? minPrice, double? maxPrice)
    {
        var query = new GetProductsQuery(minPrice, maxPrice, new List<string>(), null);
        var result = await SendToHandler(query);

        result.Products.Select(p => p.Price)
            .Should().OnlyContain(x => minPrice != null ? x > minPrice : true);

        result.Products.Select(p => p.Price)
            .Should().OnlyContain(x => maxPrice != null ? x <= maxPrice : true);
    }

    [Theory]
    [InlineData(0.00, 0.00)]
    [InlineData(90.00, 100.00)]
    public async Task Should_ReturnNoProducts(double? minPrice, double? maxPrice)
    {
        var query = new GetProductsQuery(minPrice, maxPrice, new List<string>(), null);
        var result = await SendToHandler(query);

        result.Products.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Small")]
    [InlineData("Medium")]
    [InlineData("Large")]
    public async Task Should_FilterBySize(string size)
    {
        var query = new GetProductsQuery(null, null, new List<string>(), size);
        var result = await SendToHandler(query);

        result.Products.Select(p => p.Sizes)
            .Should().OnlyContain(x => x.Contains(size));
    }

    [Theory]
    [InlineData("red")]
    [InlineData("red", "blue")]
    [InlineData("rEd", "bLUe")]
    public async Task Should_Highlight(params string[] highlights)
    {
        var query = new GetProductsQuery(null, null, highlights.ToList(), null);
        var result = await SendToHandler(query);

        foreach (var desc in result.Products.Select(p => p.Description))
        {
            var tag = AppConstants.Highlight.HighlightTag;
            foreach (var highlight in highlights)
            {
                if (desc.ToLower().Contains(highlight.ToLower()))
                {
                    desc.ToLower().Should().Contain($"<{tag}>{highlight.ToLower()}</{tag}>");
                }
            }
        }
    }

    private async Task<GetProductsDto> SendToHandler(GetProductsQuery query)
    {
        var cacheProviderMock = ICacheProviderMock.GetMock();
        var mapperMock = IMapperMock.GetMock();

        var handler = new GetProductsQueryHandler(cacheProviderMock.Object, mapperMock.Object);

        return await handler.Handle(query, new CancellationToken());
    }
}