using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class ServiceExtensions
    {
        public static void AddDbDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(provider =>
            {
                var optionBuilder = new DbContextOptionsBuilder<UserContext>();
                optionBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("MyWebsite"));
                return optionBuilder.Options;
            });

            services.AddTransient<IAuthenticationData, DbAuthenticationData>();
            services.AddDbContext<UserContext>();
        }
    }
}
