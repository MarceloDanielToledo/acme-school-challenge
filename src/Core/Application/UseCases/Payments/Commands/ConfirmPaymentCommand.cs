using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Shared.Payment.Models;
using Shared.Payment.Responses;

namespace Application.UseCases.Payments.Commands
{
    public class ConfirmPaymentCommand(Guid paymentId, Card card) : IRequest<Response<Payment>>
    {
        public Guid PaymentId { get; } = paymentId;
        public Card Card { get; } = card;
    }
    public class ConfirmPaymentCommandHandler(IPaymentService paymentService) : IRequestHandler<ConfirmPaymentCommand, Response<Payment>>
    {
        private readonly IPaymentService _paymentService = paymentService;
        public async Task<Response<Payment>> Handle(ConfirmPaymentCommand command, CancellationToken cancellationToken)
        {
            var externalResponse = await _paymentService.ConfirmPaymentRequest(command.PaymentId, command.Card);
            if (!externalResponse.IsSuccess)
            {
                return Response<Payment>.NotSuccess(externalResponse.Message);
            }
            return Response<Payment>.Success(externalResponse.Data);
        }
    }

}
