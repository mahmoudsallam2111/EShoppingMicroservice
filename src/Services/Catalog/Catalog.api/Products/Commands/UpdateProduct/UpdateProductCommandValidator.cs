namespace Catalog.api.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {

            RuleFor(p => p.Id).NotEmpty().WithMessage("product Id Can not Be empty");
            RuleFor(p => p.Name).NotEmpty().WithMessage("product Name Can not Be empty");
            RuleFor(p => p.Category).NotEmpty().WithMessage("product category Can not Be empty");
            RuleFor(p => p.ImageFile).NotEmpty().WithMessage("product Image File Can not Be empty");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("product Price Can Not Be Less Than 0");
        }
    }
}
