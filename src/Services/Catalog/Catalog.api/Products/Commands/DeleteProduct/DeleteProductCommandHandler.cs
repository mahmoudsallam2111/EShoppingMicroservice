
namespace Catalog.api.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);
    internal class DeleteProductCommandHandler (IDocumentSession session)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var productToDelete = await session.LoadAsync<Product>(request.id);
            if (productToDelete == null) { throw new productNotFountException(request.id); }

            session.Delete(productToDelete);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(IsDeleted: true);

        }
    }
}
