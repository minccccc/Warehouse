using Application.Common.Constants;
using FluentValidation;

namespace Application.Features.Queries.GetProducts;

public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(p => p.MinPrice)
            .Must(m => m == null || m >= 0)
            .WithMessage(AppConstants.Validations.MinPrice_Negative);

        RuleFor(p => p.MaxPrice)
            .Must(m => m == null || m >= 0)
            .WithMessage(AppConstants.Validations.MaxPrice_Negative);

        RuleFor(product => product)
            .Must(product => !product.MinPrice.HasValue || !product.MaxPrice.HasValue || product.MinPrice < product.MaxPrice)
            .WithMessage(AppConstants.Validations.MinPrice_LessThanMaxPrice);
    }
}