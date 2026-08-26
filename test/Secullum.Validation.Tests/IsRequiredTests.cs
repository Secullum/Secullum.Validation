using System;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsRequiredTests : BaseTest
    {
        [Fact]
        public void IsRequired_GivenValidString_DontReturnErrors()
        {
            var person = new Person() { Name = "Fernando" };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Empty(errors);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("        ")]
        public void IsRequired_GivenEmptyString_ReturnError(string name)
        {
            var person = new Person() { Name = name };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Single(errors);
            Assert.Equal("Name", errors[0].Property);
        }

        [Fact]
        public void IsRequired_GivenValidInt_DontReturnErrors()
        {
            var person = new Person() { Age = 21 };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Age)
                .ToList();

            Assert.Empty(errors);
        }

        [Fact]
        public void IsRequired_GivenEmptyInt_ReturnError()
        {
            var person = new Person();

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Age)
                .ToList();

            Assert.Single(errors);
            Assert.Equal("Age", errors[0].Property);
        }

        [Fact]
        public void IsRequired_GivenValidDateTime_DontReturnErrors()
        {
            var person = new Person() { Birth = new DateTime(1990, 5, 15) };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Birth)
                .ToList();

            Assert.Empty(errors);
        }

        [Fact]
        public void IsRequired_GivenEmptyDateTime_ReturnError()
        {
            var person = new Person() { Birth = new DateTime() };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Birth)
                .ToList();

            Assert.Single(errors);
            Assert.Equal("Birth", errors[0].Property);
        }

        [Fact]
        public void IsRequired_GivenNullDateTime_ReturnError()
        {
            var person = new Person() { Death = null };

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Death)
                .ToList();

            Assert.Single(errors);
            Assert.Equal("Death", errors[0].Property);
        }

        [Fact]
        public void IsRequired_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsRequired(x => "")
                    .ToList();
            });
        }

        [Fact]
        public void IsRequired_GivenNullInt_ReturnError()
        {
            var person = new Person();

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Zipcode)
                .ToList();

            Assert.Single(errors);
            Assert.Equal("Zipcode", errors[0].Property);
        }
    }
}
