namespace Application.UseCases.Students.Requests
{
    public class CreateStudentRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
