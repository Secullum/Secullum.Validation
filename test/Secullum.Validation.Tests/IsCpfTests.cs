using System;
using System.Globalization;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsCpfTests : BaseTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("602.154.852-38")]
        [InlineData("60215485238")]
        public void IsCpf_GivenValidField_DontReturnErrors(string cpf)
        {
            var person = new Person() { Cpf = cpf };

            var errors = new Validation<Person>(person)
                .IsCpf(x => x.Cpf)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData("diego")]
        [InlineData("000.000.000-00")]
        [InlineData("999.999.999-99")]
        [InlineData("234.580.234.75")]
        [InlineData("00000000000")]
        [InlineData("99999999999")]
        [InlineData("23458023475")]
        [InlineData("23458023475234805384")]
        [InlineData("           ")]
        public void IsCpf_GivenInvalidField_ReturnError(string cpf)
        {
            var person = new Person() { Cpf = cpf };

            var errors = new Validation<Person>(person)
                .IsCpf(x => x.Cpf)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Cpf", errors[0].Property);
        }
        
        [Fact]
        public void IsCpf_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();
            
            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsCpf(x => "")
                    .ToList();
            });
        }
    }
}
