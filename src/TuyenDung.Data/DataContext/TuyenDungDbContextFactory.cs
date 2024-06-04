using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TuyenDung.Data.DataContext
{
    public class TuyenDungDbContextFactory : IDesignTimeDbContextFactory<MyDb>
    {
        public MyDb CreateDbContext(string[] args) 
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("DB");

            var optionsBuilder = new DbContextOptionsBuilder<MyDb>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MyDb(optionsBuilder.Options);
        }
    }
}
