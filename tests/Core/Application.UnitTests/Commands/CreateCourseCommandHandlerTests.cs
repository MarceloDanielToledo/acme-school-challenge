using Application.Interfaces;
using Application.UseCases.Courses.Commands;
using Application.UseCases.Courses.Requests;
using Application.UseCases.Courses.Responses;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Commands
{
    public class CreateCourseCommandHandlerTests
    {
        private readonly Mock<IRepositoryAsync<Course>> _repositoryAsyncMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateCourseCommandHandlerTests()
        {
            _repositoryAsyncMock = new();
            _mapperMock = new();


        }

        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenNameAlreadyExist()
        {
            // Arrange
            var command = new CreateCourseCommand(new CreateCourseRequest()
            {
                Price = 10,
                Name = "Test",
                StartOn = DateTime.Now,
                EndOn = DateTime.Now,
            });

            _repositoryAsyncMock.Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<ISpecification<Course>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Course());

            var handler = new CreateCourseCommandHandler(_repositoryAsyncMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenCourseIsCreatedSuccessfully()
        {
            // Arrange
            var command = new CreateCourseCommand(new CreateCourseRequest()
            {
                Price = 10,
                Name = "UniqueName",
                StartOn = DateTime.Now.AddDays(1),
                EndOn = DateTime.Now.AddDays(10),
            });

            _repositoryAsyncMock.Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<ISpecification<Course>>(), It.IsAny<CancellationToken>())).ReturnsAsync((Course)null);

            var newCourse = new Course { Id = 1, Name = "UniqueName", Price = 10, StartOn = DateTime.Now.AddDays(1), EndOn = DateTime.Now.AddDays(10) };

            _mapperMock.Setup(m => m.Map<Course>(command.Request)).Returns(newCourse);
            _repositoryAsyncMock.Setup(x => x.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>())).ReturnsAsync(newCourse);
            _mapperMock.Setup(m => m.Map<CourseResponse>(newCourse)).Returns(new CourseResponse { Id = newCourse.Id, Name = newCourse.Name });

            var handler = new CreateCourseCommandHandler(_repositoryAsyncMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("UniqueName", result.Data.Name);
        }

        [Fact]
        public async Task Handle_Should_CallAddAsync_WhenCreatingNewCourse()
        {
            // Arrange
            var command = new CreateCourseCommand(new CreateCourseRequest()
            {
                Price = 10,
                Name = "NewCourse",
                StartOn = DateTime.Now.AddDays(1),
                EndOn = DateTime.Now.AddDays(10),
            });

            _repositoryAsyncMock.Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<ISpecification<Course>>(), It.IsAny<CancellationToken>())).ReturnsAsync((Course)null);

            _repositoryAsyncMock.Setup(x => x.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Course());

            var handler = new CreateCourseCommandHandler(_repositoryAsyncMock.Object, _mapperMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _repositoryAsyncMock.Verify(x => x.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()), Times.Once);
        }



    }
}
