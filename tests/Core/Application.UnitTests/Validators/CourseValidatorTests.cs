using Application.UseCases.Courses.Requests;
using Application.UseCases.Courses.Validators;

namespace Application.UnitTests.Validators
{
    public class CourseValidatorTests
    {
        private readonly CreateCourseValidator _validator;

        public CourseValidatorTests()
        {
            _validator = new CreateCourseValidator();
        }

        [Fact]
        public void Name_Is_Empty_Should_Fail_Validation()
        {
            var request = new CreateCourseRequest { Name = "" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El nombre del curso es obligatorio.");
        }

        [Fact]
        public void Name_Exceeds_Max_Length_Should_Fail_Validation()
        {
            var request = new CreateCourseRequest { Name = new string('a', 201) };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El nombre del curso debe tener entre 1 y 200 caracteres.");
        }

        [Fact]
        public void Name_Within_Valid_Length_Should_Pass_Validation()
        {
            var request = new CreateCourseRequest { Name = "Curso de Programación",Price = 10,StartOn = DateTime.Now.AddDays(1), EndOn = DateTime.Now.AddDays(2)};
            var result = _validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Price_Is_Zero_Should_Fail_Validation()
        {
            var request = new CreateCourseRequest { Price = 0 };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El precio debe ser mayor a 0.");
        }

        [Fact]
        public void Price_Is_Negative_Should_Fail_Validation()
        {
            var request = new CreateCourseRequest { Price = -100 };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El precio debe ser mayor a 0.");
        }


        [Fact]
        public void StartOn_Is_Today_Should_Fail_Validation()
        {
            var request = new CreateCourseRequest { StartOn = DateTime.Today };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "La fecha de inicio debe ser al menos para mañana.");
        }


        [Fact]
        public void EndOn_Is_Before_StartOn_Should_Fail_Validation()
        {
            var request = new CreateCourseRequest
            {
                StartOn = DateTime.Today.AddDays(2),
                EndOn = DateTime.Today.AddDays(1)
            };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "La fecha de finalización debe ser posterior a la fecha de inicio.");
        }
    }
}