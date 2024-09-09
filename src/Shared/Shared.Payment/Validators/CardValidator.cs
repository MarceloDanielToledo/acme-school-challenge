using FluentValidation;
using Shared.Payment.Models;
using System.Text.RegularExpressions;

namespace Shared.Payment.Validators
{
    public class CardValidator : AbstractValidator<Card>
    {
        public CardValidator() 
        {
            RuleFor(card => card.Number)
                .NotEmpty().WithMessage("El número de la tarjeta es obligatorio.")
                .Length(13, 19).WithMessage("El número de la tarjeta debe tener entre 13 y 19 dígitos.")
                .Must(BeAValidCardNumber).WithMessage("Número de tarjeta inválido.");

            RuleFor(card => card.HolderName)
                .NotEmpty().WithMessage("El nombre del titular de la tarjeta es obligatorio.")
                .Length(2, 50).WithMessage("El nombre del titular de la tarjeta debe tener entre 2 y 50 caracteres.");

            RuleFor(card => card.Expiration)
                .NotEmpty().WithMessage("El vencimiento es obligatorio.");

            RuleFor(card => card.CVV)
                .NotEmpty().WithMessage("El CVV es obligatorio.")
                .Matches(@"^\d{3,4}$").WithMessage("El CVV debe tener 3 o 4 dígitos.");

        }
        private bool BeAValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (!Regex.IsMatch(cardNumber, @"^\d+$"))
                return false;

            return IsValidLuhn(cardNumber);
        }
        private bool IsValidLuhn(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Card>.CreateWithOptions((Card)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return [];
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
