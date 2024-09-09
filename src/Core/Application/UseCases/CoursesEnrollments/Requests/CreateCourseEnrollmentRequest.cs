namespace Application.UseCases.CoursesEnrollments.Requests
{
    public class CreateCourseEnrollmentRequest(int courseId, int studentId)
    {
        public int CourseId { get; } = courseId;
        public int StudentId { get; } = studentId;
    }
}
