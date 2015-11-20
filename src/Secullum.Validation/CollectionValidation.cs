using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;

namespace Secullum.Validation
{
    public class CollectionValidation<T> : IValidation<T, CollectionValidation<T>> where T : class
    {
        private ICollection<Validation<T>> validationCollection;
                
        public CollectionValidation(ICollection<T> targetCollection) : this(targetCollection, null)
        {
        }

        public CollectionValidation(ICollection<T> targetCollection, DbContext dbContext)
        {
            validationCollection = new List<Validation<T>>();

            foreach (var item in targetCollection)
            {
                validationCollection.Add(new Validation<T>(item, dbContext));
            }
        }
        
        public CollectionValidation<T> HasDisplayText(Expression<Func<T, string>> expression, string displayText)
        {
            foreach (var validation in validationCollection)
            {
                validation.HasDisplayText(expression, displayText);
            }

            return this;
        }

        public CollectionValidation<T> HasMaxLength(Expression<Func<T, string>> expression, int maxLength)
        {
            foreach (var validation in validationCollection)
            {
                validation.HasMaxLength(expression, maxLength);
            }

            return this;
        }

        public CollectionValidation<T> IsCnpj(Expression<Func<T, string>> expression)
        {
            foreach (var validation in validationCollection)
            {
                validation.IsCnpj(expression);
            }

            return this;
        }

        public CollectionValidation<T> IsCpf(Expression<Func<T, string>> expression)
        {
            foreach (var validation in validationCollection)
            {
                validation.IsCpf(expression);
            }

            return this;
        }

        public CollectionValidation<T> IsEmail(Expression<Func<T, string>> expression)
        {
            foreach (var validation in validationCollection)
            {
                validation.IsEmail(expression);
            }

            return this;
        }

        public CollectionValidation<T> IsRequired(Expression<Func<T, string>> expression)
        {
            foreach (var validation in validationCollection)
            {
                validation.IsRequired(expression);
            }

            return this;
        }

        public CollectionValidation<T> IsUnique(Expression<Func<T, string>> expression)
        {
            foreach (var validation in validationCollection)
            {
                validation.IsUnique(expression);
            }

            return this;
        }

        public IList<IList<ValidationError>> ToList()
        {
            return validationCollection
                .Select(v => v.ToList())
                .ToList();
        }
    }
}
