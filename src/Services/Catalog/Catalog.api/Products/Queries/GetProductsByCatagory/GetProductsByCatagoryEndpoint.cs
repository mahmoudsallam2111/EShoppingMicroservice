namespace Catalog.api.Products.Queries.GetProductsByCatagory
{
    public record GetProductsByCatagoryResponse(IEnumerable<Product> Product);
    public class GetProductsByCatagoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}"  , async (string category , ISender sender ) =>
            {
               // var query = categoryName.Adapt<GetProductsByCatagoryQuery>();

                var products = await sender.Send(new GetProductsByCatagoryQuery(category));
                var response = products.Adapt<GetProductsByCatagoryResponse>();

                return Results.Ok(response);
                
            }).WithName("GetProductsByCatagory")
            .Produces<GetProductsByCatagoryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Catagory")
            .WithDescription("Get Products By Catagory");
        }
    }
}
