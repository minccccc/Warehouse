using Application.Common.Constants;
using Application.Features.Queries.GetProducts;
using Application.Models.DTOs;
using FluentAssertions;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Features;

public class GetProductsHandlerTests
{
    [Fact]
    public async Task Should_ReturnAllProducts()
    {
        //Arrange
        var query = new GetProductsQuery(null, null, new List<string>(), null);

        //Act
        var result = await SendToHandler(query);

        //Assert
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
        //Arrange
        var query = new GetProductsQuery(minPrice, maxPrice, new List<string>(), null);

        //Act
        var result = await SendToHandler(query);

        //Assert
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
        //Arrange
        var query = new GetProductsQuery(minPrice, maxPrice, new List<string>(), null);

        //Act
        var result = await SendToHandler(query);

        //Assert
        result.Products.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Small")]
    [InlineData("Medium")]
    [InlineData("Large")]
    public async Task Should_FilterBySize(string size)
    {
        //Arrange
        var query = new GetProductsQuery(null, null, new List<string>(), size);

        //Act
        var result = await SendToHandler(query);

        //Assert
        result.Products.Select(p => p.Sizes)
            .Should().OnlyContain(x => x.Contains(size));
    }

    [Theory]
    [InlineData("red")]
    [InlineData("red", "blue")]
    [InlineData("rEd", "bLUe")]
    public async Task Should_Highlight(params string[] highlights)
    {
        //Arrange
        var query = new GetProductsQuery(null, null, highlights.ToList(), null);

        //Act
        var result = await SendToHandler(query);

        //Assert
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
        var cacheProviderMock = CacheProviderMock.GetMock();
        var mapperMock = MapperMock.GetMock();

        var sut = new GetProductsQueryHandler(cacheProviderMock.Object, mapperMock.Object);

        return await sut.Handle(query, new CancellationToken());
    }
}