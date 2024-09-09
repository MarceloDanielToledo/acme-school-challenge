using Application.Interfaces;
using Application.UseCases.Courses.Commands;
using Application.UseCases.Courses.Mappings;
using Application.UseCases.Courses.Requests;
using Application.UseCases.Payments.Commands;
using Application.UseCases.Students.Commands;
using Application.UseCases.Students.Mappings;
using Application.UseCases.Students.Requests;
using AutoMapper;
using Domain.Entities;
using Infraestructure.ExternalPayment.Services;
using Infraestructure.Repository.Contexts;
using Infraestructure.Repository.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Shared.Payment.Models;
using Shared.Services;

namespace Application.IntegrationTests
{
    public class CommandsTests
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CommandsTests()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("IntegrationTestsDatabase"));

            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddScoped<IPaymentService, MockPaymentService>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddAutoMapper(typeof(Program));

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }
        [Fact]
        public async Task CreateCourseCommandHandler_Should_CreateCourse()
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var repository = new RepositoryAsync<Course>(dbContext);
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new CourseProfile())).CreateMapper();
            var handler = new CreateCourseCommandHandler(repository, mapper);

            var command = new CreateCourseCommand(new CreateCourseRequest
            {
                Name = "Programación",
                Price = 100,
                StartOn = DateTime.Today.AddDays(1),
                EndOn = DateTime.Today.AddDays(30)
            });

            var result = await handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Programación", result.Data.Name);
        }

        [Fact]
        public async Task CreateStudentCommandHandler_Should_CreateStudent()
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var repository = new RepositoryAsync<Student>(dbContext);
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new StudentProfiles())).CreateMapper();
            var handler = new CreateStudentCommandHandler(repository, mapper);

            var command = new CreateStudentCommand(new CreateStudentRequest
            {
                Name = "Alice",
                Email = "alice@example.com",
                DateOfBirth = DateTime.Today.AddYears(-20)
            });

            var result = await handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Alice", result.Data.Name);
        }

        [Fact]
        public async Task CreatePaymentCommandHandler_Should_CreatePayment()
        {
            using var scope = _scopeFactory.CreateScope();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var handler = new CreatePaymentCommandHandler(paymentService);

            var command = new CreatePaymentCommand(100);

            var result = await handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(100, result.Data.Amount);
        }

        [Fact]
        public async Task ConfirmPaymentCommandHandler_Should_ConfirmPayment()
        {
            using var scope = _scopeFactory.CreateScope();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var handler = new ConfirmPaymentCommandHandler(paymentService);

            var paymentId = Guid.NewGuid();
            var card = new Card();

            var command = new ConfirmPaymentCommand(paymentId, card);

            var result = await handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(paymentId, result.Data.Id);
        }
    }
}