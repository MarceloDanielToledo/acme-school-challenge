using Application.UseCases.CoursesEnrollments.Responses;

namespace Application.UseCases.Courses.Responses
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public ICollection<CourseEnrollmentResponse> CourseEnrollments { get; set; }
        public bool ShowItems { get; set; } = false;
    }
}
