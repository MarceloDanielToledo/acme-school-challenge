using Application.Interfaces;
using Infraestructure.ExternalPayment.Services;
using Moq;
using Shared.Payment.Enums;
using Shared.Payment.Models;

namespace ExternalPayment.UnitTests
{
    public class MockPaymentServiceTests
    {
        private readonly Mock<IDateTimeService> _dateTimeServiceMock;
        private readonly MockPaymentService _paymentService;

        public MockPaymentServiceTests()
        {
            _dateTimeServiceMock = new Mock<IDateTimeService>();
            _paymentService = new MockPaymentService(_dateTimeServiceMock.Object);
        }

        [Fact]
        public async Task ConfirmPaymentRequest_Should_ReturnSuccessResponse()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var card = new Card(); // Use a valid card or mock if needed
            var now = DateTime.UtcNow;
            _dateTimeServiceMock.Setup(d => d.Now).Returns(now);

            // Act
            var result = await _paymentService.ConfirmPaymentRequest(paymentId, card);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Success response", result.Message);
            Assert.Equal(paymentId, result.Data.Id);
            Assert.Equal(EnumPayment.InProgress, result.Data.Status);
            Assert.Equal(now, result.Data.CreatedOn);
            Assert.Equal(now.AddMinutes(5), result.Data.ExpiredOn);
        }

        [Fact]
        public async Task CreatePaymentRequest_Should_ReturnSuccessResponse()
        {
            // Arrange
            var amount = 100m;
            var now = DateTime.UtcNow;
            _dateTimeServiceMock.Setup(d => d.Now).Returns(now);

            // Act
            var result = await _paymentService.CreatePaymentRequest(amount);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Success response", result.Message);
            Assert.Equal(amount, result.Data.Amount);
            Assert.Equal(EnumPayment.Pending, result.Data.Status);
            Assert.Equal(now, result.Data.CreatedOn);
            Assert.Equal(now.AddMinutes(5), result.Data.ExpiredOn);
        }

        [Fact]
        public async Task GetStatus_Should_ReturnSuccessResponse_WithRandomStatus()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var now = DateTime.UtcNow;
            _dateTimeServiceMock.Setup(d => d.Now).Returns(now);

            // Act
            var result = await _paymentService.GetStatus(paymentId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Success response", result.Message);
            Assert.Equal(paymentId, result.Data.Id);
            Assert.Equal(now, result.Data.CreatedOn);
            Assert.Equal(now.AddMinutes(5), result.Data.ExpiredOn);

            // Check that the status is one of the enum values
            var statusValues = Enum.GetValues(typeof(EnumPayment));
            Assert.Contains(result.Data.Status, statusValues.Cast<EnumPayment>());
        }
    }
}