using EmployeeManagement.Contracts.Interfaces.Repositories;
using EmployeeManagement.Contracts.Interfaces.Services;
using EmployeeManagement.Repository.Repositories;
using EmployeeManagement.Service.Services;

namespace EmployeeManagement.Api.Configuration
{
    public static class ServicesConfiguration
    {

        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IJobPositionService, JobPositionService>();

            //Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IJobPositionRepository, JobPositionRepository>();
            services.AddScoped<IStorageRepository, StorageRepository>();

            return services;
        }

    }
}
