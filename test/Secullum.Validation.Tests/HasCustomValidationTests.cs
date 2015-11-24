using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class HasCustomValidationTests : BaseTest, IClassFixture<PeopleDbContext>
    {
        private PeopleDbContext context;
        
        public HasCustomValidationTests(PeopleDbContext context)
        {
            this.context = context;
        }
        
        [Fact]
        public void HasCustomValidation_GivenSimpleFalseCondition_ReturnErrors()
        {
            var person = new Person();

            var errors = new Validation<Person>(person, context)
                .HasCustomValidation(x => false, "Email", "Preencha o campo Email corretamente")
                .ToList();

            Assert.Equal("Preencha o campo Email corretamente", errors[0].Message);
        }
        
        [Fact]
        public void HasCustomValidation_GivenTrueCondition_DontReturnErrors()
        {
            var person = new Person() { Id = 2, Age = 20, Email = "unknown@domain.net" };

            var errors = new Validation<Person>(person, context)
                .HasCustomValidation(x => true, "Email", "Preencha o campo Email corretamente")
                .ToList();

            Assert.Equal(0, errors.Count);
        }

        [Fact]
        public void HasCustomValidation_GivenFalseCondition_ReturnErrors()
        {
            var person = new Person() { Id = 2, Age = 20, Email = "unknown@domain.net" };

            var errors = new Validation<Person>(person, context)
                .HasCustomValidation(x => !((x.Email == "unknown@domain.net" || x.Id == 1) && x.Age > 0), "Email", "Preencha o campo Email corretamente")
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Preencha o campo Email corretamente", errors[0].Message);
        }
    }
}
