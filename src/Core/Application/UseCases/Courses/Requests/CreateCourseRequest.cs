namespace Application.UseCases.Courses.Requests
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }
    }
}
