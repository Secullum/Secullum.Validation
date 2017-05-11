using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsCepTests : BaseTest
    {
        [Theory]
        [InlineData("93700-000")]
        [InlineData("93700000")]
        [InlineData(null)]
        [InlineData("")]
        public void IsCep_GivenValidField_DontReturnErrors(string cep)
        {
            var person = new Person() { Cep = cep };

            var errors = new Validation<Person>(person)
                .IsCep(x => x.Cep)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData("93700-00")]
        [InlineData("9370000000")]
        [InlineData("9370000")]
        [InlineData("asdfads")]
        [InlineData("           ")]
        public void IsCep_GivenInvalidField_ReturnError(string cep)
        {
            var person = new Person() { Cep = cep };

            var errors = new Validation<Person>(person)
                .IsCep(x => x.Cep)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Cep", errors[0].Property);
        }

        [Fact]
        public void IsCep_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsCep(x => "")
                    .ToList();
            });
        }
    }
}
