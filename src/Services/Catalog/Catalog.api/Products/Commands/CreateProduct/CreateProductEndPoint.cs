namespace Catalog.api.Products.Commands.CreateProduct;

public record CreateProductrequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record CreateProductResponse(Guid Id);
public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductrequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = sender.Send(command);
            var response = result.Result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create product")
            .WithDescription("Create product");
    }
}
