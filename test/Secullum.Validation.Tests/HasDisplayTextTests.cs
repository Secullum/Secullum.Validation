using System;
using System.Globalization;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class HasDisplayTextTests : BaseTest
    {
        [Fact]
        public void HasDisplayText_NotGivenDisplayText_UsesPropertyName()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Equal("Name", errors[0].Property);
            Assert.Equal($"O campo Name é obrigatório.", errors[0].Message);
        }

        [Fact]
        public void HasDisplayText_GivenDisplayText_UsesIt()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .HasDisplayText(x => x.Name, "Nome")
                .IsRequired(x => x.Name)
                .ToList();

            Assert.Equal("Name", errors[0].Property);
            Assert.Equal($"O campo Nome é obrigatório.", errors[0].Message);
        }

        [Fact]
        public void HasDisplayText_GivenInvalidExpression_ThrowsException()
        {
            var person = new Person();

            Assert.Throws<ArgumentException>("expression", () =>
            {
                var erros = new Validation<Person>(person)
                    .IsRequired(x => "")
                    .ToList();
            });
        }
    }
}
