using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class IsBetweenTests : BaseTest, IClassFixture<PeopleDbContext>
    {
        private PeopleDbContext context;

        public IsBetweenTests(PeopleDbContext context)
        {
            this.context = context;
        }

        [Theory]
        [InlineData(18)]
        [InlineData(19)]
        [InlineData(25)]
        [InlineData(50)]
        [InlineData(60)]
        public void IsBetween_GivenValidIntValue_DontReturnErrors(int age)
        {
            var person = new Person() { Id = 2, Age = age };

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Age, 18, 60)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        [InlineData(17)]
        [InlineData(61)]
        [InlineData(100)]
        public void IsBetween_GivenOutOfRangeIntValue_ReturnErrors(int age)
        {
            var person = new Person() { Id = 2, Age = age };

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Age, 18, 60)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Age", errors[0].Property);
            Assert.Equal("O campo Age deve ser preenchido com valores entre 18 e 60.", errors[0].Message);
        }

        [Theory]
        [InlineData(100.00)]
        [InlineData(101.01)]
        [InlineData(125.50)]
        [InlineData(234.567)]
        [InlineData(250.00)]
        public void IsBetween_GivenValidFloatValue_DontReturnErrors(int height)
        {
            var person = new Person() { Id = 2, Height = height};

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Height, 100, 250)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(15.10)]
        [InlineData(17)]
        [InlineData(61.55)]
        [InlineData(99.999)]
        [InlineData(300)]
        public void IsBetween_GivenOutOfRangeFloatValue_ReturnErrors(int height)
        {
            var person = new Person() { Id = 2, Height = height};

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Height, (float)100.01, (float)250.02)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Height", errors[0].Property);
            Assert.Equal("O campo Height deve ser preenchido com valores entre 100,01 e 250,02.", errors[0].Message);
        }

        [Theory]
        [InlineData(10000)]
        [InlineData(12345)]
        [InlineData(22222)]
        [InlineData(77788)]
        [InlineData(99999)]
        public void IsBetween_GivenValidNullableIntValue_DontReturnErrors(int zipcode)
        {
            var person = new Person() { Id = 2, Age = zipcode };

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Age, 10000, 99999)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(15)]
        [InlineData(17)]
        [InlineData(61)]
        [InlineData(100000)]
        [InlineData(999999)]
        public void IsBetween_GivenOutOfRangeNullableIntValue_ReturnErrors(int zipcode)
        {
            var person = new Person() { Id = 2, Zipcode = zipcode };

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Zipcode, 10000, 99999)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Zipcode", errors[0].Property);
            Assert.Equal("O campo Zipcode deve ser preenchido com valores entre 10000 e 99999.", errors[0].Message);
        }

        [Theory]
        [InlineData(100.00)]
        [InlineData(101.01)]
        [InlineData(125.50)]
        [InlineData(234.567)]
        [InlineData(250.00)]
        public void IsBetween_GivenValidNullableFloatValue_DontReturnErrors(int weight)
        {
            var person = new Person() { Id = 2, Weight = weight };

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Weight, 100, 250)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(15.10)]
        [InlineData(17)]
        [InlineData(61.55)]
        [InlineData(99.999)]
        [InlineData(300)]
        public void IsBetween_GivenOutOfRangeNullableFloatValue_ReturnErrors(int weight)
        {
            var person = new Person() { Id = 2, Weight  = weight };

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Weight, (float)100.01, (float)250.02)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Weight", errors[0].Property);
            Assert.Equal("O campo Weight deve ser preenchido com valores entre 100,01 e 250,02.", errors[0].Message);
        }

        [Fact]
        public void IsBetween_GivenNullableIntValue_DontReturnErrors()
        {
            var person = new Person() { Id = 2};

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Zipcode, 18, 60)
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Fact]
        public void IsBetween_GivenNullableFloatValue_DontReturnErrors()
        {
            var person = new Person() { Id = 2 };

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Weight , 18, 60)
                .ToList();

            Assert.Equal(0, errors.Count);
        }
    }
}
