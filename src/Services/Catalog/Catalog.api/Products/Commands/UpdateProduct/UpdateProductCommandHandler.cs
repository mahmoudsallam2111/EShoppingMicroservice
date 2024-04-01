namespace Catalog.api.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool Issuccessed);

    internal class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var productToUpdate = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (productToUpdate is null)
                throw new productNotFountException(request.Id);


            productToUpdate.Name = request.Name;
            productToUpdate.Category = request.Category;
            productToUpdate.Description = request.Description;
            productToUpdate.ImageFile = request.ImageFile;
            productToUpdate.Price = request.Price;

             session.Update(productToUpdate);    

             await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(Issuccessed: true);

        }
    }
}
