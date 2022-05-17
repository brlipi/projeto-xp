using Microsoft.EntityFrameworkCore;
using projeto_xp.Api.Models;

namespace projeto_xp.Api.Services
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialization(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<UserContext>().Database.Migrate();
            }
        }
    }
}
