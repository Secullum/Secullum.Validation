using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Secullum.Validation
{
    interface IValidation<T> where T : class
    {
        Validation<T> HasDisplayText(Expression<Func<T, string>> expression, string displayText);
        Validation<T> IsRequired(Expression<Func<T, string>> expression);
        Validation<T> HasMaxLength(Expression<Func<T, string>> expression, int maxLength);
        Validation<T> IsEmail(Expression<Func<T, string>> expression);
        Validation<T> IsUnique(Expression<Func<T, string>> expression);
        Validation<T> IsCpf(Expression<Func<T, string>> expression);
        Validation<T> IsCnpj(Expression<Func<T, string>> expression);
    }
}
