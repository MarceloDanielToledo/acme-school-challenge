using Application.Constant;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Wrappers
{
    public class ResponseTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var data = new { Id = 1, Name = "Test" };
            var response = new Response<object>(true, "Operation successful", new List<string>(), data);

            Assert.True(response.IsSuccess);
            Assert.Equal("Operation successful", response.Message);
            Assert.Empty(response.Errors);
            Assert.Equal(data, response.Data);
        }

        [Fact]
        public void NotSuccess_ShouldReturnFailedResponse()
        {
            var response = Response<object>.NotSuccess("Operation failed");

            Assert.False(response.IsSuccess);
            Assert.Equal("Operation failed", response.Message);
            Assert.Empty(response.Errors);
            Assert.Null(response.Data);
        }

        [Fact]
        public void NotSuccess_WithErrors_ShouldReturnFailedResponseWithErrors()
        {
            var errors = new[] { "Error 1", "Error 2" };
            var response = Response<object>.NotSuccess("Operation failed", errors);

            Assert.False(response.IsSuccess);
            Assert.Equal("Operation failed", response.Message);
            Assert.Equal(errors, response.Errors);
            Assert.Null(response.Data);
        }

        [Fact]
        public void Success_ShouldReturnSuccessfulResponseWithMessage()
        {
            var data = new { Id = 1, Name = "Test" };
            var response = Response<object>.Success(data, "Operation successful");

            Assert.True(response.IsSuccess);
            Assert.Equal("Operation successful", response.Message);
            Assert.Empty(response.Errors);
            Assert.Equal(data, response.Data);
        }

        [Fact]
        public void Success_ShouldReturnSuccessfulResponseWithoutMessage()
        {
            var data = new { Id = 1, Name = "Test" };
            var response = Response<object>.Success(data);

            Assert.True(response.IsSuccess);
            Assert.Equal(ResponseMessages.SuccessMessage, response.Message);
            Assert.Empty(response.Errors);
            Assert.Equal(data, response.Data);
        }
    }

}
