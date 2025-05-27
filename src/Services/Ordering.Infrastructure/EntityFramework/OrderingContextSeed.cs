using Microsoft.AspNetCore.Hosting;
namespace Ordering.Infrastructure.EntityFramework;

public class OrderingContextSeed : IDbSeeder<OrderingContext>
{
    public async Task SeedAsync(OrderingContext context)
    {
        await context.SaveChangesAsync();
    }

}