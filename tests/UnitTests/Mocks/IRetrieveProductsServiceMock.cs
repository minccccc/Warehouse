﻿using Domain.Models;
using Infrastructure.Http;
using Moq;

namespace UnitTests.Mocks;

public static class IRetrieveProductsServiceMock
{
    public static Mock<IRetrieveProductsService> GetMock()
    {
        var retrieveProductMock = new Mock<IRetrieveProductsService>();
        retrieveProductMock.Setup(m => m.GetProducts<List<Product>>("", new CancellationToken()))
            .ReturnsAsync(new List<Product>()
            {
                new Product()
                {
                    Title = "New Product1",
                    Price = 24.99,
                    Sizes = new List<string>() { "Small", "Medium", "Large" },
                    Description = "Product 1 description : Comfortable cotton t-shirt in red with a stylish design."
                },
                new Product()
                {
                    Title = "New Product2",
                    Price = 34.99,
                    Sizes = new List<string>() { "Small", "Medium" },
                    Description = "Product 2 description: Timeless classic jeans in blue for a casual and versatile look."
                },
                new Product()
                {
                    Title = "New Product3",
                    Price = 44.99,
                    Sizes = new List<string>() { "Medium", "Large" },
                    Description = "Product 3 description : Warm and cozy hoodie in green for a relaxed and comfortable style."
                }
            });

        return retrieveProductMock;
    }
}