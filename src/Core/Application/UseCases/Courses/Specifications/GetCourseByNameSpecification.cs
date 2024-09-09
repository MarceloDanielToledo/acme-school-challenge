using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.Courses.Specifications
{
    public class GetCourseByNameSpecification : Specification<Course>
    {
        public GetCourseByNameSpecification(string name) 
        {
            Query.Where(x => x.Name.ToUpper() == name.ToUpper()).AsNoTracking();
        }

    }
}
