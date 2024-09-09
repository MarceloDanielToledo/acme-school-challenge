using Application.Interfaces;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTimeService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTimeService) : base(options) => _dateTimeService = dateTimeService;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = _dateTimeService.Now;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrollment> CoursesEnrollments { get; set; }

    }
}
