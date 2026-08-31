using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsTimespanTests : BaseTest
    {
        [Theory]
        [InlineData("99:00")]
        [InlineData("10:35")]
        [InlineData("00:03")]
        [InlineData("1:39")]
        [InlineData("187:39")]
        [InlineData(null)]
        [InlineData("")]
        public void IsTimespan_GivenValidField_DontReturnErrors(string timespan)
        {
            var person = new Person() { Hour = timespan };

            var errors = new Validation<Person>(person)
                .IsTimespan(x => x.Hour)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData("99:99")]
        [InlineData("10:69")]
        [InlineData("asdfads")]
        [InlineData("           ")]
        public void IsTimespan_GivenInvalidField_ReturnError(string timespan)
        {
            var person = new Person() { Hour = timespan };

            var errors = new Validation<Person>(person)
                .IsTimespan(x => x.Hour)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Hour", errors[0].Property);
        }

        [Fact]
        public void IsTimespan_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsTimespan(x => "")
                    .ToList();
            });
        }
    }
}
