using Microsoft.EntityFrameworkCore;

namespace TranslationManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // automatic startup database migration
            var scope = host.Services.GetService<IServiceScopeFactory>()?.CreateScope();
            var dbContext = scope?.ServiceProvider.GetRequiredService<AppDbContext>();
            if (dbContext == null)
            {
                throw new ArgumentNullException("Scope or AppDbContext cannot be null");
            }
            dbContext.Database.Migrate();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
