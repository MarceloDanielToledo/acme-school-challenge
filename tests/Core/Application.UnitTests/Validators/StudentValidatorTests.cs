using Application.UseCases.Students.Requests;
using Application.UseCases.Students.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Validators
{
    public class StudentValidatorTests
    {
        private readonly CreateStudentValidator _validator;

        public StudentValidatorTests()
        {
            _validator = new CreateStudentValidator();
        }

        [Fact]
        public void Name_Is_Empty_Should_Fail_Validation()
        {
            var request = new CreateStudentRequest { Name = "" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El nombre del estudiante es obligatorio.");
        }

        [Fact]
        public void Name_Exceeds_Max_Length_Should_Fail_Validation()
        {
            var request = new CreateStudentRequest { Name = new string('a', 101) }; // 101 characters
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El nombre del estudiante debe tener entre 1 y 100 caracteres.");
        }


        [Fact]
        public void Email_Is_Empty_Should_Fail_Validation()
        {
            var request = new CreateStudentRequest { Email = "" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El correo electrónico es obligatorio.");
        }

        [Fact]
        public void Email_Invalid_Format_Should_Fail_Validation()
        {
            var request = new CreateStudentRequest { Email = "invalid-email" };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Formato de correo electrónico inválido.");
        }

        [Fact]
        public void Email_Exceeds_Max_Length_Should_Fail_Validation()
        {
            var request = new CreateStudentRequest { Email = new string('a', 101) + "@example.com" }; // 101+ characters
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El correo electrónico debe tener entre 1 y 100 caracteres.");
        }

        [Fact]
        public void DateOfBirth_Is_Empty_Should_Fail_Validation()
        {
            var request = new CreateStudentRequest { DateOfBirth = default(DateTime) };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "La fecha de nacimiento es obligatoria.");
        }

        [Fact]
        public void DateOfBirth_Less_Than_18_Years_Should_Fail_Validation()
        {
            var request = new CreateStudentRequest { DateOfBirth = DateTime.Today.AddYears(-17) };
            var result = _validator.Validate(request);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "El estudiante debe tener al menos 18 años.");
        }

    }
}
