using Application.UseCases.Courses.Requests;
using FluentValidation;

namespace Application.UseCases.Courses.Validators
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseRequest>
    {
        public CreateCourseValidator()
        {
            RuleFor(course => course.Name)
                .NotEmpty().WithMessage("El nombre del curso es obligatorio.")
                .Length(1, 200).WithMessage("El nombre del curso debe tener entre 1 y 200 caracteres.");

            RuleFor(course => course.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");

            RuleFor(course => course.StartOn)
                .GreaterThanOrEqualTo(DateTime.Today.AddDays(1)).WithMessage("La fecha de inicio debe ser al menos para mañana.");

            RuleFor(course => course.EndOn)
                .GreaterThan(course => course.StartOn).WithMessage("La fecha de finalización debe ser posterior a la fecha de inicio.");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateCourseRequest>.CreateWithOptions((CreateCourseRequest)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return [];
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
