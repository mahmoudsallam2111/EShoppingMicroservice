namespace Catalog.api.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.id).NotEmpty().WithMessage("product Id Can not Be empty");
        }
    }
}
