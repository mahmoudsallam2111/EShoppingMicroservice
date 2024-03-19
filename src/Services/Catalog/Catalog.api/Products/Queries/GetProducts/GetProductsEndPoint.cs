namespace Catalog.api.Products.Queries.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async (ISender sender) =>
            {
                var products = await sender.Send(new GetProductsQuery());
                var response = products.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            }).WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}
