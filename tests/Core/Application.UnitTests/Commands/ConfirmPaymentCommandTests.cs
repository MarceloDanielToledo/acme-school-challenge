using Application.Constant;
using Application.Interfaces;
using Application.UseCases.Payments.Commands;
using Application.Wrappers;
using Moq;
using Shared.Payment.Models;
using Shared.Payment.Responses;
using Shared.Payment.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Commands
{
    public   class ConfirmPaymentCommandTests
    {
        private readonly Mock<IPaymentService> _paymentServiceMock;

        public ConfirmPaymentCommandTests()
        {
            _paymentServiceMock = new();
        }
        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenPaymentConfirmationFails()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var card = new Card { Number = "4111111111111111", Expiration = "1225"};
            var command = new ConfirmPaymentCommand(paymentId, card);

            var paymentResponse = new PaymentResponse<Payment>
            {
                IsSuccess = false,
                Message = "Payment confirmation failed"
            };

            _paymentServiceMock.Setup(x => x.ConfirmPaymentRequest(paymentId, card))
                .ReturnsAsync(paymentResponse);

            var handler = new ConfirmPaymentCommandHandler(_paymentServiceMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("Payment confirmation failed", result.Message);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenPaymentConfirmationSucceeds()
        {
            // Arrange
            var paymentId = Guid.NewGuid();
            var card = new Card { Number = "4111111111111111", Expiration = "1225" };
            var command = new ConfirmPaymentCommand(paymentId, card);
            var payment = new Payment();
            var paymentResponse = new PaymentResponse<Payment>
            {
                IsSuccess = true,
                Message = ResponseMessages.SuccessMessage,
                Data = payment
            };

            _paymentServiceMock.Setup(x => x.ConfirmPaymentRequest(paymentId, card))
                .ReturnsAsync(paymentResponse);

            var handler = new ConfirmPaymentCommandHandler(_paymentServiceMock.Object);

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
