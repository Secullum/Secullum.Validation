using System;
using System.Globalization;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsRequiredTests : BaseTest
    {
        [Fact]
        public void IsRequired_GivenValidField_DontReturnErrors()
        {
            var person = new Person() { Name = "Fernando" };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("        ")]
        public void IsRequired_GivenEmptyField_ReturnError(string name)
        {
            var person = new Person() { Name = name };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Name", errors[0].Property);
        }

        [Theory]
        [InlineData(null, "Name")]
        [InlineData("Nome", "Nome")]
        [InlineData("aaa", "aaa")]
        public void IsRequired_GivenPropertyDisplayText_ReturnsCustomizedMessage(string propertyDisplayText, string expectedPropertyDisplayText)
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name, propertyDisplayText)
                .ToList();

            Assert.Equal($"O campo {expectedPropertyDisplayText} é obrigatório.", errors[0].Message);
        }

        [Fact]
        public void IsRequired_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();
            
            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsRequired(x => "")
                    .ToList();
            });
        }
    }
}
