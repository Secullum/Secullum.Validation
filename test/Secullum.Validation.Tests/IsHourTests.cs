using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsHourTests : BaseTest
    {
        [Theory]
        [InlineData("10:35")]
        [InlineData("00:03")]
        [InlineData(null)]
        [InlineData("")]
        public void IsHour_GivenValidField_DontReturnErrors(string hour)
        {
            var person = new Person() { Hour = hour };

            var errors = new Validation<Person>(person)
                .IsHour(x => x.Hour)
                .ToList();

            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("99:00")]
        [InlineData("99:99")]
        [InlineData("10:69")]
        [InlineData("1:39")]
        [InlineData("asdfads")]
        [InlineData("           ")]
        public void IsHour_GivenInvalidField_ReturnError(string hour)
        {
            var person = new Person() { Hour = hour };

            var errors = new Validation<Person>(person)
                .IsHour(x => x.Hour)
                .ToList();

            Assert.Single(errors);
            Assert.Equal("Hour", errors[0].Property);
        }

        [Fact]
        public void IsHour_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsHour(x => "")
                    .ToList();
            });
        }
    }
}
