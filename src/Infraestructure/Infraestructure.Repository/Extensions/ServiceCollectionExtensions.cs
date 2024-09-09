using Application.Interfaces;
using Infraestructure.Repository.Contexts;
using Infraestructure.Repository.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("MemoryDb"))
                .AddTransient<IDatabaseSeeder, DatabaseSeeder>();
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

        }
    }
}
