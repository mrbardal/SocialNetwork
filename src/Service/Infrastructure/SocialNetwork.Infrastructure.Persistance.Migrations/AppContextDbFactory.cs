using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SocialNetwork.Infrastructure.Persistance;

namespace Catalog.Infrastructure.Persistance.Migrations
{
    public class AppContextDbFactory : IDesignTimeDbContextFactory<AppContextDb>
    {
        public AppContextDb CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<AppContextDb>();

            var connectionString = configuration.GetConnectionString("SocialNetwork");

            dbContextBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("SocialNetwork.Infrastructure.Persistance.Migrations"));

            return new AppContextDb(dbContextBuilder.Options);
        }
    }
}
