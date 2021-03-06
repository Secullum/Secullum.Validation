﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Secullum.Validation.Tests
{
    public class CollectionTests : BaseTest
    {
        [Fact]
        public void IsRequired_GivenValidField_DontReturnErrors()
        {
            var people = new List<Person>();

            people.Add(new Person { Name = "Fernando" });
            people.Add(new Person { Name = "Alex" });
            people.Add(new Person { Name = "Diego" });


            var errors = new CollectionValidation<Person>(people)
                .IsRequired(x => x.Name)
                .ToList();

            foreach (var lista in errors)
            {
                Assert.Equal(0, lista.Count);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("        ")]
        public void IsRequired_GivenEmptyField_ReturnError(string name)
        {
            var people = new List<Person>();

            people.Add(new Person { Name = name });
            
            var errors = new CollectionValidation<Person>(people)
                .IsRequired(x => x.Name)
                .ToList();

            foreach (var lista in errors)
            {
                Assert.Equal(1, lista.Count);
                Assert.Equal("Name", lista[0].Property);
            }
        }

        [Theory]
        [InlineData("Fernando", 8)]
        [InlineData("Fernando", 100)]
        [InlineData(null, 100)]
        [InlineData("", 100)]
        public void HasMaxLength_GivenValidField_DontReturnErrors(string name, int maxLength)
        {
            var people = new List<Person>();

            people.Add(new Person { Name = name });

            var errors = new CollectionValidation<Person>(people)
                .HasMaxLength(x => x.Name, maxLength)
                .ToList();

            foreach (var lista in errors)
            {
                Assert.Equal(0, lista.Count);
            }
        }

        [Fact]
        public void HasMaxLength_GivenInvalidField_ReturnError()
        {
            var people = new List<Person>();

            people.Add(new Person { Name = "Fernando" });
            
            var errors = new CollectionValidation<Person>(people)
                .HasMaxLength(x => x.Name, 5)
                .ToList();

            Assert.Equal(1, errors.Count);
            Assert.Equal("Name", errors[0][0].Property);
        }

        [Fact]
        public void HasCustomValidation_GivenFalseCondition_ReturnErrors()
        {
            var people = new List<Person>();

            people.Add(new Person());

            var errors = new CollectionValidation<Person>(people)
                .HasCustomValidation(x => false, "Email", "Preencha o campo Email corretamente")
                .ToList();

            Assert.Equal(1, errors[0].Count);
            Assert.Equal("Preencha o campo Email corretamente", errors[0][0].Message);
        }

        [Theory]
        [InlineData(2015, 10, 10)]
        [InlineData(2000, 10, 15)]
        [InlineData(1935, 10, 10)]
        [InlineData(2040, 10, 10)]
        public void IsSmallDateTime_GivenValidField_DontReturnErrors(int year, int month, int day)
        {
            var people = new List<Person>();

            people.Add(new Person() { Birth = new DateTime(year, month, day) });

            var errors = new CollectionValidation<Person>(people)
                .IsSmallDateTime(x => x.Birth)
                .ToList();

            Assert.Equal(0, errors[0].Count);
        }

    }
}
