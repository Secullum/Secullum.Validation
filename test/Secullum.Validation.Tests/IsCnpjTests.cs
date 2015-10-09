using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsCnpjTests : BaseTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("99.582.412/0001-44")]
        [InlineData("99582412000144")]
        public void IsCnpj_GivenValidField_DontReturnErrors(string cnpj)
        {
            var person = new Person() { Cnpj = cnpj };

            var errors = new Validation<Person>(person)
                .IsCnpj(x => x.Cnpj)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData("diego")]
        [InlineData("00.000.000/0000-00")]
        [InlineData("99.999.999/9999-99")]
        [InlineData("12.345.678/9012-34")]
        [InlineData("00000000000000")]
        [InlineData("99999999999999")]
        [InlineData("23458023475545")]
        [InlineData("23458023475234805384")]
        [InlineData("              ")]
        public void IsCnpj_GivenInvalidField_ReturnError(string cnpj)
        {
            var person = new Person() { Cnpj = cnpj };

            var errors = new Validation<Person>(person)
                .IsCnpj(x => x.Cnpj)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Cnpj", errors[0].Property);
        }
        
        [Fact]
        public void IsCnpj_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();
            
            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsCnpj(x => "")
                    .ToList();
            });
        }
    }
}
