using Application.Interfaces;
using Application.UseCases.CoursesEnrollments.Commands;
using Application.UseCases.CoursesEnrollments.Requests;
using Application.UseCases.CoursesEnrollments.Responses;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Commands
{
    public class CreateCourseEnrollmentCommandTests
    {
        private readonly Mock<IRepositoryAsync<CourseEnrollment>> _repositoryAsyncMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateCourseEnrollmentCommandTests()
        {
            _repositoryAsyncMock = new();
            _mapperMock = new();
        }
        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenEnrollmentIsCreatedSuccessfully()
        {
            // Arrange
            var command = new CreateCourseEnrollmentCommand(new CreateCourseEnrollmentRequest(1,1));

            _repositoryAsyncMock.Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<ISpecification<CourseEnrollment>>(), It.IsAny<CancellationToken>())).ReturnsAsync((CourseEnrollment)null);

            var newEnrollment = new CourseEnrollment { Id = 1, CourseId = 1, StudentId = 1 };

            _mapperMock.Setup(m => m.Map<CourseEnrollment>(command.Request)).Returns(newEnrollment);
            _repositoryAsyncMock.Setup(x => x.AddAsync(It.IsAny<CourseEnrollment>(), It.IsAny<CancellationToken>())).ReturnsAsync(newEnrollment);
            _mapperMock.Setup(m => m.Map<CourseEnrollmentResponse>(newEnrollment)).Returns(new CourseEnrollmentResponse {CourseId = newEnrollment.CourseId, StudentId = newEnrollment.StudentId });

            var handler = new CreateCourseEnrollmentCommandHandler(_repositoryAsyncMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(newEnrollment.CourseId, result.Data.CourseId);
            Assert.Equal(newEnrollment.StudentId, result.Data.StudentId);
        }
        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenEnrollmentAlreadyExists()
        {
            // Arrange
            var command = new CreateCourseEnrollmentCommand(new CreateCourseEnrollmentRequest(1,1));

            _repositoryAsyncMock.Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<ISpecification<CourseEnrollment>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new CourseEnrollment());

            var handler = new CreateCourseEnrollmentCommandHandler(_repositoryAsyncMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("El alumno ya se encuentra inscripto al curso ingresado.", result.Message);
        }
        [Fact]
        public async Task Handle_Should_CallAddAsync_WhenCreatingNewEnrollment()
        {
            // Arrange
            var command = new CreateCourseEnrollmentCommand(new CreateCourseEnrollmentRequest(1,1));

            _repositoryAsyncMock.Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<ISpecification<CourseEnrollment>>(), It.IsAny<CancellationToken>())).ReturnsAsync((CourseEnrollment)null);

            _repositoryAsyncMock.Setup(x => x.AddAsync(It.IsAny<CourseEnrollment>(), It.IsAny<CancellationToken>())).ReturnsAsync(new CourseEnrollment());

            var handler = new CreateCourseEnrollmentCommandHandler(_repositoryAsyncMock.Object, _mapperMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _repositoryAsyncMock.Verify(x => x.AddAsync(It.IsAny<CourseEnrollment>(), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
