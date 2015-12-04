using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsSmallDateTimeTests : BaseTest
    {
        [Theory]
        [InlineData(2015, 10, 10)]
        [InlineData(2000, 10, 15)]
        [InlineData(1935, 10, 10)]
        [InlineData(2040, 10, 10)]
        public void HasSmallDateTime_GivenValidField_DontReturnErrors(int year, int month, int day)
        {
            var person = new Person() { Birth = new DateTime(year, month, day) };

            var errors = new Validation<Person>(person)
                .IsSmallDateTime(x => x.Birth)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData(1899, 12, 31)]
        [InlineData(1800, 10, 15)]
        [InlineData(0005, 10, 10)]
        [InlineData(2079, 10, 10)]
        [InlineData(2999, 10, 10)]
        public void HasSmallDateTime_GivenInvalidField_ReturnError(int year, int month, int day)
        {
            var person = new Person() { Birth = new DateTime(year, month, day) };

            var errors = new Validation<Person>(person)
                            .IsSmallDateTime(x => x.Birth)
                            .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Birth", errors[0].Property);
        }
    }
}
