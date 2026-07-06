using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsEmailTests : BaseTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("fernando@domain.com")]
        [InlineData("fernando@domain.com.br")]
        public void IsEmail_GivenValidField_DontReturnErrors(string email)
        {
            var person = new Person() { Email = email };

            var errors = new Validation<Person>(person)
                .IsEmail(x => x.Email)
                .ToList();

            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("fernando")]
        [InlineData("fernando@domain")]
        [InlineData("@.com")]
        [InlineData("           ")]
        public void IsEmail_GivenInvalidField_ReturnError(string email)
        {
            var person = new Person() { Email = email };

            var errors = new Validation<Person>(person)
                .IsEmail(x => x.Email)
                .ToList();

            Assert.Single(errors);
            Assert.Equal("Email", errors[0].Property);
        }

        [Fact]
        public void IsEmail_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsEmail(x => "")
                    .ToList();
            });
        }
    }
}
