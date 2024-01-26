﻿using Application.Models;
using AutoMapper;
using Domain.Models;
using Moq;

namespace UnitTests.Mocks;

public static class IMapperMock
{

    public static Mock<IMapper> GetMock()
    {
        var mapperMock = new Mock<IMapper>();

        mapperMock
            .Setup(m => m.Map<GetProductsDto>(It.IsAny<ProductsSummary>()))
            .Returns((ProductsSummary p) =>
            {
                return new GetProductsDto()
                {
                    Summary = p
                };
            });

        return mapperMock;
    }
}
