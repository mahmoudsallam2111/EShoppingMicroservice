using Microsoft.Extensions.Logging;

namespace Catalog.api.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name , List<string> Category , string Description , string ImageFile , decimal Price)
  :ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price,
        };

        // save this entity to db
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        // return the result
        return new CreateProductResult(product.Id);

    }
}
