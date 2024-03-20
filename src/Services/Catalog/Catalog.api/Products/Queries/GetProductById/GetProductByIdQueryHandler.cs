namespace Catalog.api.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler (IDocumentSession session , ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetproductsQuery Handler called with {@Query} ", query);

            var product =await session.Query<Product>()
                .SingleOrDefaultAsync(p => p.Id == query.id);

            if (product is null)
                throw new productNotFountException(query.id);

            return new GetProductByIdResult(product);
        }
    }
}
