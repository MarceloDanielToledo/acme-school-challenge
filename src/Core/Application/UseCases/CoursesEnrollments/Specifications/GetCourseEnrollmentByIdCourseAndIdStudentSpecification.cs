using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.CoursesEnrollments.Specifications
{
    public class GetCourseEnrollmentByIdCourseAndIdStudentSpecification : Specification<CourseEnrollment>
    {
        public GetCourseEnrollmentByIdCourseAndIdStudentSpecification(int courseId, int studentId) 
        {
            Query.Where(x => x.CourseId == courseId && x.StudentId == studentId).AsNoTracking();
        }
    }
}
