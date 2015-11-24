using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class GenericValidationTests : BaseTest, IClassFixture<PeopleDbContext>
    {
        private PeopleDbContext context;
        
        public GenericValidationTests(PeopleDbContext context)
        {
            this.context = context;
        }

        [Fact]
        public void GenericValidation_GivenFalseCondition_ReturnErrors()
        {
            var person = new Person() { Id = 2, Age = 20, Email = "unknown@domain.net" };

            var errors = new Validation<Person>(person, context)
                .GenericValidation(x => (x.Email == "unknown@domain.net" || x.Id == 1) && x.Age > 0, "Email", "Preencha o campo Email corretamente")
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Preencha o campo Email corretamente", errors[0].Message);
        }

        [Fact]
        public void GenericValidation_GivenTrueCondition_DontReturnErrors()
        {
            var person = new Person() { Id = 2, Age = 20, Email = "unknown@domain.net" };

            var errors = new Validation<Person>(person, context)
                .GenericValidation(x => (x.Email == "known@domain.net" || x.Id == 1) && x.Age > 0, "Email", "Preencha o campo Email corretamente")
                .ToList();

            Assert.Equal(0, errors.Count);
        }

    }
}
