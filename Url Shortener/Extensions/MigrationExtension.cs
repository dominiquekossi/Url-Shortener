using Microsoft.EntityFrameworkCore;
using Url_Shortener.Infrastructure;

namespace Url_Shortener.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope  = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
