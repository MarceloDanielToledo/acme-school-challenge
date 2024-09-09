using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Repository.Contexts;
using Infraestructure.Repository.Seeder;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Repository.UnitTests
{
    public class RepositoryAsyncTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRepositoryAsync<Student> _studentRepository;
        private readonly IRepositoryAsync<Course> _courseRepository;

        public RepositoryAsyncTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApplicationDbContext(options, new Mock<IDateTimeService>().Object);
            _studentRepository = new RepositoryAsync<Student>(_dbContext);
            _courseRepository = new RepositoryAsync<Course>(_dbContext);
        }
        [Fact]
        public async Task Add_Should_AddEntityToDatabase()
        {
            // Arrange
            var student = new Student { Name = "John Doe", Email = "john.doe@example.com" };

            // Act
            await _studentRepository.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            // Assert
            var addedStudent = await _studentRepository.GetByIdAsync(student.Id);
            Assert.NotNull(addedStudent);
            Assert.Equal("John Doe", addedStudent.Name);
        }

        [Fact]
        public async Task Update_Should_UpdateEntityInDatabase()
        {
            // Arrange
            var course = new Course { Name = "Programación", Price = 50, StartOn = DateTime.Today.AddDays(1), EndOn = DateTime.Today.AddDays(30) };
            await _courseRepository.AddAsync(course);
            await _dbContext.SaveChangesAsync();

            // Act
            course.Name = "Programación";
            await _courseRepository.UpdateAsync(course);
            await _dbContext.SaveChangesAsync();

            // Assert
            var updatedCourse = await _courseRepository.GetByIdAsync(course.Id);
            Assert.NotNull(updatedCourse);
            Assert.Equal("Programación", updatedCourse.Name);
        }

        [Fact]
        public async Task Delete_Should_RemoveEntityFromDatabase()
        {
            // Arrange
            var student = new Student { Name = "Jane Doe", Email = "jane.doe@example.com" };
            await _studentRepository.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            // Act
            await _studentRepository.DeleteAsync(student);
            await _dbContext.SaveChangesAsync();

            // Assert
            var deletedStudent = await _studentRepository.GetByIdAsync(student.Id);
            Assert.Null(deletedStudent);
        }

        [Fact]
        public async Task GetAll_Should_ReturnAllEntities()
        {
            // Arrange
            var student1 = new Student { Name = "John Doe", Email = "john.doe@example.com" };
            var student2 = new Student { Name = "Jane Doe", Email = "jane.doe@example.com" };
            await _studentRepository.AddAsync(student1);
            await _studentRepository.AddAsync(student2);
            await _dbContext.SaveChangesAsync();

            // Act
            var students = await _studentRepository.ListAsync();

            // Assert
            Assert.NotNull(students);
            Assert.Equal(3, students.Count());
        }
    }
}
