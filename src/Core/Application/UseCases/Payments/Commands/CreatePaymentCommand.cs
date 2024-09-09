using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Shared.Payment.Responses;

namespace Application.UseCases.Payments.Commands
{
    public class CreatePaymentCommand(decimal amount) : IRequest<Response<Payment>>
    {
        public decimal Amount { get; } = amount;
    }
    public class CreatePaymentCommandHandler(IPaymentService paymentService) : IRequestHandler<CreatePaymentCommand, Response<Payment>>
    {
        private readonly IPaymentService _paymentService = paymentService;

        public async Task<Response<Payment>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var externalResponse = await _paymentService.CreatePaymentRequest(command.Amount);
            if (!externalResponse.IsSuccess)
            {
                return Response<Payment>.NotSuccess(externalResponse.Message);
            }
            return Response<Payment>.Success(externalResponse.Data);
        }
    }


}
