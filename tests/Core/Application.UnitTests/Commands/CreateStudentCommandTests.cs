using Application.Constant;
using Application.Interfaces;
using Application.UseCases.Students.Commands;
using Application.UseCases.Students.Requests;
using Application.UseCases.Students.Responses;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Commands
{
    public class CreateStudentCommandTests
    {
        private readonly Mock<IRepositoryAsync<Student>> _repositoryAsyncMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateStudentCommandTests()
        {
            _repositoryAsyncMock = new Mock<IRepositoryAsync<Student>>();
            _mapperMock = new Mock<IMapper>();
        }
        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenStudentWithEmailExists()
        {
            // Arrange
            var command = new CreateStudentCommand(new CreateStudentRequest
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Today.AddYears(-20)
            });

            var existingStudent = new Student(); 
            _repositoryAsyncMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Student>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingStudent);

            var handler = new CreateStudentCommandHandler(_repositoryAsyncMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("Ya existe un estudiante con el email ingresado.", result.Message);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenStudentCreationSucceeds()
        {
            // Arrange
            var request = new CreateStudentRequest
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Today.AddYears(-20)
            };

            var command = new CreateStudentCommand(request);
            var student = new Student(); 
            var studentResponse = new StudentResponse();
            var responseMessage = ResponseMessages.AddedSuccesfullyMessage;

            _repositoryAsyncMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Student>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Student)null); 
            _mapperMock.Setup(m => m.Map<Student>(It.IsAny<CreateStudentRequest>())).Returns(student);
            _repositoryAsyncMock.Setup(x => x.AddAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(student);
            _mapperMock.Setup(m => m.Map<StudentResponse>(It.IsAny<Student>())).Returns(studentResponse);

            var handler = new CreateStudentCommandHandler(_repositoryAsyncMock.Object, _mapperMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(responseMessage, result.Message);
            Assert.Equal(studentResponse, result.Data);
        }
    }
}
