using Application.Features.Products.Query;
using FluentValidation;

namespace Application.Validators
{
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(product => product)
                .Must(product => !product.MinPrice.HasValue || !product.MaxPrice.HasValue || product.MinPrice < product.MaxPrice)
                .WithMessage("MinPrice must be less than MaxPrice");
        }
    }
}
