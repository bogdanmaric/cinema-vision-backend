using MovieApi.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieApi.Tests
{
    public class RegisterDtoTests
    {
        [Fact]
        public void Should_Fail_When_Email_Is_Missing()
        {
            var dto = new RegisterDto
            {
                Username = "testuser",
                Password = "password123"
                // Email is missing
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
        }
    }
}
