using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsRequiredTests
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

        [Fact]
        public void IsRequired_GivenNull_ReturnError()
        {
            var person = new Person() { Name = null };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Name", errors[0].Property);
        }

        [Fact]
        public void IsRequired_GivenEmpty_ReturnError()
        {
            var person = new Person() { Name = "" };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Name", errors[0].Property);
        }

        [Fact]
        public void IsRequired_GivenSpaces_ReturnError()
        {
            var person = new Person() { Name = "         " };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Name", errors[0].Property);
        }

        [Fact]
        public void IsRequired_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();
            
            Assert.Throws<ArgumentException>(() =>
            {
                var erros = new Validation<Person>(person)
                    .IsRequired(x => "")
                    .ToList();
            });
        }
    }
}
