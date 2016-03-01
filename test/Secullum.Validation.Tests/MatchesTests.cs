using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class MatchesTests : BaseTest
    {
        [Theory]
        [InlineData("99954454")]
        [InlineData("0")]
        [InlineData("")]
        [InlineData(null)]
        public void Matches_GivenValidString_DontReturnErrors(string pis)
        {
            var person = new Person() { Pis = pis };

            var errors = new Validation<Person>(person)
                .Matches(x => x.Pis, @"^\d+$", "Error {0}")
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("           ")]
        public void Matches_GivenInvalidString_ReturnError(string pis)
        {
            var person = new Person() { Pis = pis };

            var errors = new Validation<Person>(person)
                .Matches(x => x.Pis, @"^\d+$", "Error {0}")
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Pis", errors[0].Property);
        }

        [Fact]
        public void Matches_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .Matches(x => "", "", "")
                    .ToList();
            });
        }
    }
}
