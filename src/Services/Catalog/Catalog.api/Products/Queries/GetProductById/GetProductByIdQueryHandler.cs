namespace Catalog.api.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler (IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product =await session.Query<Product>()
                .SingleOrDefaultAsync(p => p.Id == query.id);

            if (product is null)
                throw new productNotFountException(query.id);

            return new GetProductByIdResult(product);
        }
    }
}
