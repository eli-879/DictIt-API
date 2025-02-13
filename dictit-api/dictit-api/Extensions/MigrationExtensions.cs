using DictItApi.Database;
using Microsoft.EntityFrameworkCore;

namespace DictItApi.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using DictItDbContext context = scope.ServiceProvider.GetRequiredService<DictItDbContext>();

        var pendingMigrations = context.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
        {
            context.Database.Migrate();
        }
    }
}
