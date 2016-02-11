using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsPisTests : BaseTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("000000013-2")]
        [InlineData("132")]
        public void IsPis_GivenValidField_DontReturnErrors(string pis)
        {
            var person = new Person() { Pis = pis };

            var errors = new Validation<Person>(person)
                .IsPis(x => x.Pis)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData("000000000-0")]
        [InlineData("999999999-9")]
        [InlineData("345802347-5")]
        [InlineData("0000000000")]
        [InlineData("99999999999")]
        [InlineData("3458023475")]
        [InlineData("23458023475234805384")]
        [InlineData("           ")]
        public void IsPis_GivenInvalidField_ReturnError(string pis)
        {
            var person = new Person() { Pis = pis };

            var errors = new Validation<Person>(person)
                .IsPis(x => x.Pis)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Pis", errors[0].Property);
        }

        [Fact]
        public void IsPis_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsPis(x => "")
                    .ToList();
            });
        }
    }
}
