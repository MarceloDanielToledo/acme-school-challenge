using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Repository.Contexts;

namespace Infraestructure.Repository.Seeder
{
    public class DatabaseSeeder(ApplicationDbContext db) : IDatabaseSeeder
    {
        private readonly ApplicationDbContext _db = db;

        public async Task Initialize()
        {
            await AddCourses();
            await AddStudents();
            await AddCoursesEnrollments();
            _db.SaveChanges();
        }
        private async Task AddCourses()
        {
            List<Course> courses = [];
            var mathsCourse = new Course()
            {
                Name = "Matemáticas Avanzadas",
                Price = 100,
                StartOn = DateTime.Now,
                CreatedOn = DateTime.Now,
                EndOn = DateTime.Now.AddDays(10),
            };
            var programmingCourse = new Course()
            {
                Name = "Introducción a la Programación",
                Price = 500,
                StartOn = DateTime.Now,
                CreatedOn = DateTime.Now,
                EndOn = DateTime.Now.AddDays(20),
            };
            var proyectManagementCourse = new Course()
            {
                Name = "Gestión de Proyectos",
                Price = 100,
                StartOn = DateTime.Now,
                CreatedOn = DateTime.Now,
                EndOn = DateTime.Now.AddDays(10),
            };
            courses.Add(mathsCourse);
            courses.Add(programmingCourse);
            courses.Add(proyectManagementCourse);
            await _db.Courses.AddRangeAsync(courses);
        }
        private async Task AddStudents()
        {
            List<Student> students = [];
            var newStudent = new Student()
            {
                Name = "Daniel Toledo",
                Email = "daniel@email.com",
                DateOfBirth = DateTime.Now.AddYears(-20),
                CreatedOn = DateTime.Now
            };
            var newStudent2 = new Student()
            {
                Name = "Nicolas Perez",
                Email = "nicolas@email.com",
                DateOfBirth = DateTime.Now.AddDays(-5).AddYears(-20),
                CreatedOn = DateTime.Now
            };
            var newStudent3 = new Student()
            {
                Name = "Fernando Rodriguez",
                Email = "fernando@email.com",
                DateOfBirth = DateTime.Now.AddYears(-25),
                CreatedOn = DateTime.Now
            };
            students.Add(newStudent);
            students.Add(newStudent2);
            students.Add(newStudent3);
            await _db.Students.AddRangeAsync(students);
        }
        private async Task AddCoursesEnrollments()
        {
            List<CourseEnrollment> courseEnrollments = [];
            var newEnrollment = new CourseEnrollment()
            {
                StudentId = 1,
                CourseId = 1,
                CreatedOn = DateTime.Now
            };
            var newEnrollment2 = new CourseEnrollment()
            {
                StudentId = 1,
                CourseId = 2,
                CreatedOn = DateTime.Now
            };
            var newEnrollment3 = new CourseEnrollment()
            {
                StudentId = 2,
                CourseId = 1,
                CreatedOn = DateTime.Now
            };
            courseEnrollments.Add(newEnrollment);
            courseEnrollments.Add(newEnrollment2);
            courseEnrollments.Add(newEnrollment3);
            await _db.CoursesEnrollments.AddRangeAsync(courseEnrollments);
        }
    }
}