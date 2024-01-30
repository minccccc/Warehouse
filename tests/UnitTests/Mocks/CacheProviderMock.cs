using Application.Common.Constants;
using Application.Interfaces;
using Domain.Models;
using Moq;

namespace UnitTests.Mocks;

public static class CacheProviderMock
{
    public static Mock<ICacheProvider> GetMock()
    {
        var products = new List<Product>()
            {
                new Product()
                {
                    Title = "Stylish T-Shirt",
                    Price = 24.99M,
                    Sizes = new List<string>() { "Small", "Medium", "Large" },
                    Description = "Comfortable cotton t-shirt in red with a stylish design."
                },
                new Product()
                {
                    Title = "Classic Jeans",
                    Price = 34.99M,
                    Sizes = new List<string>() { "Small", "Medium" },
                    Description = "Timeless classic jeans in blue for a casual and versatile look."
                },
                new Product(){
                    Title= "Cozy Hoodie",
                    Price= 16.99M,
                    Sizes= new List<string> () { "Small","Medium", "Large" },
                    Description= "Warm and cozy hoodie in green for a relaxed and comfortable style."
                },
                new Product(){
                    Title = "Formal Suit",
                    Price= 59.99M,
                    Sizes= new List<string>() { "Large" },
                    Description= "Elegant formal suit in white for special occasions and business events."
                },
                new Product(){
                    Title= "Sporty Tracksuit",
                    Price= 69.99M,
                    Sizes= new List<string> { "Small", "Medium", "Large" },
                    Description= "Versatile sporty tracksuit in yellow for an active and stylish lifestyle."
                }
            };
        var productsSummary = new ProductsSummary()
        {
            Highlights = new List<string>() { "red", "green", "blue" },
            MinPrice = 16.99M,
            MaxPrice = 69.99M,
            Sizes = new List<string>() { "Small", "Medium", "Large" }
        };


        var cacheProviderMock = new Mock<ICacheProvider>();

        cacheProviderMock
            .Setup(m => m.Get<List<Product>>(AppConstants.ProductsCacheKey))
            .Returns(products);
        cacheProviderMock
            .Setup(m => m.Set<List<Product>>(AppConstants.ProductsCacheKey, It.IsAny<List<Product>>()))
            .Callback((string key, List<Product> p) =>
            {
                products.RemoveAll(pr => pr != null);
                products.AddRange(p);
            });

        cacheProviderMock.Setup(m => m.Get<ProductsSummary>(AppConstants.ProductsSummaryCacheKey))
            .Returns(productsSummary);
        cacheProviderMock
            .Setup(m => m.Set<ProductsSummary>(AppConstants.ProductsSummaryCacheKey, It.IsAny<ProductsSummary>()))
            .Callback((string key, ProductsSummary summary) =>
            {
                productsSummary.Highlights = summary.Highlights;
                productsSummary.MaxPrice = summary.MaxPrice;
                productsSummary.MinPrice = summary.MinPrice;
                productsSummary.Sizes = summary.Sizes;
            });

        return cacheProviderMock;
    }
}