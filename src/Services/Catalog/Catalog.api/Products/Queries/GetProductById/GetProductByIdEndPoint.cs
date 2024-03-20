using Microsoft.AspNetCore.Mvc;

namespace Catalog.api.Products.Queries.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}",
                async (Guid id, ISender sender) =>
                {
                    var product = await sender.Send(new GetProductByIdQuery(id));
                    var result = product.Adapt<GetProductByIdResponse>();

                    return Results.Ok(result);

                }).WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Get Product By Idt")
               .WithDescription("Get Product By Id");
        }
    }
}
