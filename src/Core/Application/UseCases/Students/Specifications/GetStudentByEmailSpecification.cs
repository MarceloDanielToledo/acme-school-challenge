using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.Students.Specifications
{
    public class GetStudentByEmailSpecification : Specification<Student>
    {
        public GetStudentByEmailSpecification(string email)
        {
            Query.Where(x => x.Email.ToUpper() == email.ToUpper()).AsNoTracking();
        }
    }
}
