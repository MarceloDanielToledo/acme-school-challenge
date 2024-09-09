using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.Courses.Specifications
{
    public class GetCoursesWithStudentsByDateRangeSpecification : Specification<Course>
    {
        public GetCoursesWithStudentsByDateRangeSpecification(DateTime start, DateTime end)
        {
            Query
                .Include(x => x.CourseEnrollments.Where(e => e.CreatedOn >= start && e.CreatedOn <= end))
                .ThenInclude(y => y.Student).AsNoTracking();

        }
    }
}
