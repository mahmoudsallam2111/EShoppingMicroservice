using BuildingBlocks.CQRS;
using Catalog.api.Models;

namespace Catalog.api.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name , List<string> Category , string Description , string ImageFile , decimal Price)
      :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = request.Name,
                Catagory = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
            };

            // save this entity to db

            // return the result
            return new CreateProductResult(Guid.NewGuid());

        }
    }
}
