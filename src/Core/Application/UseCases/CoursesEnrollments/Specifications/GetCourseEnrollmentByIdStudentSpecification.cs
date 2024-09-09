using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.CoursesEnrollments.Specifications
{
    public class GetCourseEnrollmentByIdStudentSpecification : Specification<CourseEnrollment>
    {
        public GetCourseEnrollmentByIdStudentSpecification(int studentId) 
        {
            Query.Where(x=>x.StudentId == studentId).Include(x=>x.Course).AsNoTracking();
        }

    }
}
