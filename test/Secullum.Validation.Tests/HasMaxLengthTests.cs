using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class HasMaxLengthTests
    {
        [Theory]
        [InlineData("Fernando", 8)]
        [InlineData("Fernando", 100)]
        [InlineData(null, 100)]
        [InlineData("", 100)]
        public void HasMaxLength_GivenValidField_DontReturnErrors(string name, int maxLength)
        {
            var person = new Person() { Name = name };

            var errors = new Validation<Person>(person)
                .HasMaxLength(x => x.Name, maxLength)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Fact]
        public void HasMaxLength_GivenInvalidField_ReturnError()
        {
            var person = new Person() { Name = "Fernando" };

            var errors = new Validation<Person>(person)
                .HasMaxLength(x => x.Name, 5)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Name", errors[0].Property);
        }

        [Fact]
        public void HasMaxLength_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .HasMaxLength(x => "", 100)
                    .ToList();
            });
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void HasMaxLength_GivenInvalidLength_ThrowsException(int length)
        {
            var person = new Person();

            Assert.Throws<ArgumentOutOfRangeException>("maxLength", () =>
            {
                var erros = new Validation<Person>(person)
                    .HasMaxLength(x => x.Name, length)
                    .ToList();
            });
        }
    }
}
