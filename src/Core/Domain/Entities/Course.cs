using Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Course : BaseEntity
    {
        [StringLength(200)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }
        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
    }
}
