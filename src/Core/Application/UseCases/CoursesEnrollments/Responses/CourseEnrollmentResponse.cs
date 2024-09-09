using Application.UseCases.Courses.Responses;
using Application.UseCases.Students.Responses;

namespace Application.UseCases.CoursesEnrollments.Responses
{
    public class CourseEnrollmentResponse
    {
        public int StudentId { get; set; }
        public StudentResponse Student { get; set; }
        public int CourseId { get; set; }
        public CourseResponse Course { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
