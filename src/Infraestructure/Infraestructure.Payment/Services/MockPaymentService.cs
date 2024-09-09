using Application.Interfaces;
using Shared.Payment.Enums;
using Shared.Payment.Models;
using Shared.Payment.Responses;
using Shared.Payment.Wrappers;

namespace Infraestructure.ExternalPayment.Services
{
    public class MockPaymentService(IDateTimeService dateTimeService) : IPaymentService
    {
        private readonly IDateTimeService _dateTimeService = dateTimeService;

        public async Task<PaymentResponse<Payment>> ConfirmPaymentRequest(Guid paymentId, Card card)
        {
            Random random = new();
            var delayInMilliseconds = random.Next(1, 5) * 1000;
            await Task.Delay(delayInMilliseconds);
            return await Task.FromResult(new PaymentResponse<Payment>()
            {
                IsSuccess = true,
                Message = "Success response",
                Data = new()
                {
                    Status = EnumPayment.InProgress,
                    CreatedOn = _dateTimeService.Now,
                    Amount = 100,
                    ExpiredOn = _dateTimeService.Now.AddMinutes(5),
                    Id = paymentId
                }
            });
        }

        public async Task<PaymentResponse<Payment>> CreatePaymentRequest(decimal amount)
        {
            Random random = new();
            var delayInMilliseconds = random.Next(1, 5) * 1000;
            await Task.Delay(delayInMilliseconds);
            return await Task.FromResult(new PaymentResponse<Payment>()
            {
                IsSuccess = true,
                Message = "Success response",
                Data = new()
                {
                    Status = EnumPayment.Pending,
                    Amount = amount,
                    CreatedOn = _dateTimeService.Now,
                    ExpiredOn = _dateTimeService.Now.AddMinutes(5),
                    Id = Guid.NewGuid()
                }
            });
        }
        public async Task<PaymentResponse<Payment>> GetStatus(Guid paymentId)
        {
            var payment = new Payment()
            {
                Id = paymentId,
                CreatedOn = _dateTimeService.Now,
                Amount = 100,
                ExpiredOn = _dateTimeService.Now.AddMinutes(5),
            };
            var response = new PaymentResponse<Payment>()
            {
                IsSuccess = true,
                Data = payment,
                Message = "Success response"
            };

            Random random = new();
            var delayInMilliseconds = random.Next(1, 5) * 1000;
            await Task.Delay(delayInMilliseconds);
            Array statusValues = Enum.GetValues(typeof(EnumPayment));
            response.Data.Status = (EnumPayment)statusValues.GetValue(random.Next(statusValues.Length));

            return await Task.FromResult(response);
        }
    }
}
