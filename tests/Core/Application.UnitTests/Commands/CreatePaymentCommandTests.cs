using Application.Constant;
using Application.Interfaces;
using Application.UseCases.Payments.Commands;
using Moq;
using Shared.Payment.Responses;
using Shared.Payment.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Commands
{
    public class CreatePaymentCommandTests
    {
        private readonly Mock<IPaymentService> _paymentServiceMock;

        public CreatePaymentCommandTests()
        {
            _paymentServiceMock = new Mock<IPaymentService>();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenPaymentCreationFails()
        {
            // Arrange
            var amount = 100m;
            var command = new CreatePaymentCommand(amount);

            var paymentResponse = new PaymentResponse<Payment>
            {
                IsSuccess = false,
                Message = "Payment creation failed"
            };

            _paymentServiceMock.Setup(x => x.CreatePaymentRequest(amount))
                .ReturnsAsync(paymentResponse);

            var handler = new CreatePaymentCommandHandler(_paymentServiceMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("Payment creation failed", result.Message);
        }
        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenPaymentCreationSucceeds()
        {
            // Arrange
            var amount = 100m;
            var command = new CreatePaymentCommand(amount);
            var payment = new Payment(); 
            var paymentResponse = new PaymentResponse<Payment>
            {
                IsSuccess = true,
                Message = "Payment created successfully",
                Data = payment
            };

            _paymentServiceMock.Setup(x => x.CreatePaymentRequest(amount))
                .ReturnsAsync(paymentResponse);

            var handler = new CreatePaymentCommandHandler(_paymentServiceMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(ResponseMessages.SuccessMessage, result.Message);
            Assert.Equal(payment, result.Data);
        }
    }
}
