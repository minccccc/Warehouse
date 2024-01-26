using FluentValidation;

namespace Application.Features.Queries.GetProducts;

public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(p => p.MinPrice)
            .Must(m => m == null || m >= 0)
            .WithMessage("MinPrice can not be negative");

        RuleFor(p => p.MaxPrice)
            .Must(m => m == null || m >= 0)
            .WithMessage("MaxPrice can not be negative");

        RuleFor(product => product)
            .Must(product => !product.MinPrice.HasValue || !product.MaxPrice.HasValue || product.MinPrice < product.MaxPrice)
            .WithMessage("MinPrice must be less than MaxPrice");
    }
}