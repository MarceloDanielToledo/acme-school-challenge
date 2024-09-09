using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Shared.Payment.Responses;

namespace Application.UseCases.Payments.Queries
{
    public class GetPaymentStatusQuery(Guid paymentId) : IRequest<Response<Payment>>
    {
        public Guid PaymentId { get; } = paymentId;
    }
    public class GetPaymentStatusQueryHandler(IPaymentService paymentService) : IRequestHandler<GetPaymentStatusQuery, Response<Payment>>
    {
        private readonly IPaymentService _paymentService = paymentService;

        public async Task<Response<Payment>> Handle(GetPaymentStatusQuery request, CancellationToken cancellationToken)
        {
            var externalResponse = await _paymentService.GetStatus(request.PaymentId);
            if (!externalResponse.IsSuccess)
            {
                return Response<Payment>.NotSuccess(externalResponse.Message);
            }
            return Response<Payment>.Success(externalResponse.Data);
        }
    }
}
