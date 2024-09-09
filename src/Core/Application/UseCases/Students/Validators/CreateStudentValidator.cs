using Application.UseCases.Students.Requests;
using FluentValidation;

namespace Application.UseCases.Students.Validators
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentValidator()
        {
            RuleFor(student => student.Name)
                .NotEmpty().WithMessage("El nombre del estudiante es obligatorio.")
                .Length(1, 100).WithMessage("El nombre del estudiante debe tener entre 1 y 100 caracteres.");

            RuleFor(student => student.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("Formato de correo electrónico inválido.")
                .Length(1, 100).WithMessage("El correo electrónico debe tener entre 1 y 100 caracteres.");

            RuleFor(student => student.DateOfBirth)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .Must(BeAtLeast18YearsOld).WithMessage("El estudiante debe tener al menos 18 años.");
        }
        private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age)) age--;
            return age >= 18;
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateStudentRequest>.CreateWithOptions((CreateStudentRequest)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return [];
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
