using Application.Common.Constants;
using Application.Features.Commands.SyncProducts;
using Domain.Models;
using FluentAssertions;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Features;

public class SyncProductsHandlerTests
{
    [Fact]
    public async Task Should_CacheAllProducts()
    {
        var cacheProviderMock = ICacheProviderMock.GetMock();
        var retrieveProductsMock = IRetrieveProductsServiceMock.GetMock();

        var command = new SyncProductsCommand("");
        var handler = new SyncProductsCommandHandler(retrieveProductsMock.Object, cacheProviderMock.Object);
        await handler.Handle(command, new CancellationToken());

        var cachedProducts = cacheProviderMock.Object.Get<List<Product>>(AppConstants.ProductsCacheKey);
        var retrievedProducts = await retrieveProductsMock.Object.GetProducts<List<Product>>("", new CancellationToken());

        cachedProducts.Should().Contain(retrievedProducts);
    }

    [Fact]
    public async Task Should_CalculateSummary()
    {
        var cacheProviderMock = ICacheProviderMock.GetMock();
        var retrieveProductsMock = IRetrieveProductsServiceMock.GetMock();

        var command = new SyncProductsCommand("");
        var handler = new SyncProductsCommandHandler(retrieveProductsMock.Object, cacheProviderMock.Object);
        await handler.Handle(command, new CancellationToken());

        var cachedProducts = cacheProviderMock.Object.Get<List<Product>>(AppConstants.ProductsCacheKey);
        var cachedSummary = cacheProviderMock.Object.Get<ProductsSummary>(AppConstants.ProductsSummaryCacheKey);

        cachedSummary.MinPrice.Should().Be(cachedProducts.Select(p => p.Price).Min());
        cachedSummary.MaxPrice.Should().Be(cachedProducts.Select(p => p.Price).Max());
        cachedSummary.Sizes.Should().Contain(cachedProducts.SelectMany(p => p.Sizes).Distinct());
    }
}