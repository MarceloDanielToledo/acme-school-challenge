using Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public DateTime DateOfBirth {  get; set; }
        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
    }
}
