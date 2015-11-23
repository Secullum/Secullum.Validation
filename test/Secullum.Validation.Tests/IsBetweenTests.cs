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
        public void IsBetween_GivenValidValue_DontReturnErrors(int age)
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
        public void IsBetween_GivenOutOfRangeValue_ReturnErrors(int age)
        {
            var person = new Person() { Id = 2, Age = age };

            var errors = new Validation<Person>(person, context)
                .IsBetween(x => x.Age, 18, 60)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Age", errors[0].Property);
            Assert.Equal("O campo Age deve ser preenchido com valores entre 18 e 60.", errors[0].Message);
        }
    }
}
