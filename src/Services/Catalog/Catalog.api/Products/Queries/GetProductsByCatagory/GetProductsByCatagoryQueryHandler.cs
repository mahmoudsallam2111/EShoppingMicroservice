
namespace Catalog.api.Products.Queries.GetProductsByCatagory
{
    public record GetProductsByCatagoryQuery(string Catagory) : IQuery<GetProductsByCatagoryResult>;

    public record GetProductsByCatagoryResult(IEnumerable<Product>  Product);
    internal class GetProductsByCatagoryQueryHandler (IDocumentSession session , ILogger<GetProductsByCatagoryQueryHandler> logger)
        : IQueryHandler<GetProductsByCatagoryQuery, GetProductsByCatagoryResult>
    {
        public async Task<GetProductsByCatagoryResult> Handle(GetProductsByCatagoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetproductsbycatagoryQuery Handler called with {@Query} ", query);

            var products = await session.Query<Product>()
                .Where(p=>p.Category.Contains(query.Catagory))
                .ToListAsync();

            return new GetProductsByCatagoryResult(products);   
                
        }
    }
}
