using Microsoft.EntityFrameworkCore;

namespace CharacterApi.DbContext
{
    public class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            try
            {
                using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<CharacterDbContext>();

                    if (context != null)
                    {
                        context.Database.Migrate();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
