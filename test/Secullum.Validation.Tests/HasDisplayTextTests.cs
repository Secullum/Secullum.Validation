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
        public void HasDisplayText_GivenDisplayTextIntValue_UsesIt()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .HasDisplayText(x => x.Age, "Idade")
                .IsBetween(x => x.Age, 1, 2)
                .ToList();

            Assert.Equal("Age", errors[0].Property);
            Assert.Equal($"O campo Idade deve ser preenchido com valores entre 1 e 2.", errors[0].Message);
        }

        [Fact]
        public void HasDisplayText_GivenDisplayTextNullableIntValue_UsesIt()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person() { Zipcode = 42 } ;

            var errors = new Validation<Person>(person)
                .HasDisplayText(x => x.Zipcode, "Zipcode")
                .IsBetween(x => x.Zipcode, 1, 2)
                .ToList();

            Assert.Equal("Zipcode", errors[0].Property);
            Assert.Equal($"O campo Zipcode deve ser preenchido com valores entre 1 e 2.", errors[0].Message);
        }

        [Fact]
        public void HasDisplayText_GivenDisplayTextFloatValue_UsesIt()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person() ;

            var errors = new Validation<Person>(person)
                .HasDisplayText(x => x.Height, "Height")
                .IsBetween(x => x.Height, (float)100.01, (float)250.02)
                .ToList();

            Assert.Equal("Height", errors[0].Property);
            Assert.Equal($"O campo Height deve ser preenchido com valores entre 100,01 e 250,02.", errors[0].Message);
        }
        
        [Fact]
        public void HasDisplayText_GivenDisplayTextNullableFloatValue_UsesIt()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person() {Weight = 5};

            var errors = new Validation<Person>(person)
                .HasDisplayText(x => x.Weight, "Weight")
                .IsBetween(x => x.Weight, (float)21.2, (float)200.5)
                .ToList();

            Assert.Equal("Weight", errors[0].Property);
            Assert.Equal($"O campo Weight deve ser preenchido com valores entre 21,2 e 200,5.", errors[0].Message);
        }

        [Fact]
        public void HasDisplayText_GivenDisplayTextGuidValue_UsesIt()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .HasDisplayText(x => x.GlobalId, "Global Id")
                .IsRequired(x => x.GlobalId)
                .ToList();

            Assert.Equal("GlobalId", errors[0].Property);
            Assert.Equal($"O campo Global Id é obrigatório.", errors[0].Message);
        }

        [Fact]
        public void HasDisplayText_GivenDisplayTextNullableGuidValue_UsesIt()
        {
            SetCurrentThreadCulture(new CultureInfo("pt-BR"));

            var person = new Person();

            var errors = new Validation<Person>(person)
                .HasDisplayText(x => x.UniversalId, "Universal Id")
                .IsRequired(x => x.UniversalId)
                .ToList();

            Assert.Equal("UniversalId", errors[0].Property);
            Assert.Equal($"O campo Universal Id é obrigatório.", errors[0].Message);
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