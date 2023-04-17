using EmployeeManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Configuration
{
    public static class DatabaseConfiguration
    {

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQL_CONNECTION");
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Sql Connection is required");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
