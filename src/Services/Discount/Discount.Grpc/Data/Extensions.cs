using Microsoft.EntityFrameworkCore;

// create automigration extension method 
namespace Discount.Grpc.Data
{
    public static class Extensions
    {
        public static async Task<IApplicationBuilder> UseMigrationAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            await dbContext.Database.MigrateAsync();

            return app;
        }
    }
}
