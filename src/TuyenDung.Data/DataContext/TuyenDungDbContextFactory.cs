using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
