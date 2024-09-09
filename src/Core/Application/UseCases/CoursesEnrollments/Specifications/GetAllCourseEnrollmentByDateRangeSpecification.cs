using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.CoursesEnrollments.Specifications
{
    public class GetAllCourseEnrollmentByDateRangeSpecification : Specification<CourseEnrollment>
    {
        public GetAllCourseEnrollmentByDateRangeSpecification(DateTime start, DateTime end)
        {
            Query
                .Where(e => e.CreatedOn >= start && e.CreatedOn <= end)
                .Include(enrollment => enrollment.Course)
                .Include(enrollment => enrollment.Student).AsNoTracking();
        }

    }
}
