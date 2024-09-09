using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Infraestructure.Repository.Contexts;

namespace Infraestructure.Repository.Seeder
{
    public class RepositoryAsync<T>(ApplicationDbContext dbContext) : RepositoryBase<T>(dbContext), IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext dbContext = dbContext;
    }
}
