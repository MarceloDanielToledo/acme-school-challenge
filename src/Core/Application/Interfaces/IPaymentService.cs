using Shared.Payment.Models;
using Shared.Payment.Responses;
using Shared.Payment.Wrappers;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse<Payment>> CreatePaymentRequest(decimal amount);
        Task<PaymentResponse<Payment>> ConfirmPaymentRequest(Guid paymentId, Card card);
        Task<PaymentResponse<Payment>> GetStatus(Guid paymentId);

    }
}
