using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Secullum.Validation
{
    public interface IValidation<TTarget, TValidation>
        where TTarget : class
        where TValidation : IValidation<TTarget, TValidation>
    {
        TValidation HasDisplayText(Expression<Func<TTarget, string>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, int>> expression, string displayText);
        TValidation IsRequired(Expression<Func<TTarget, string>> expression);
        TValidation HasMaxLength(Expression<Func<TTarget, string>> expression, int maxLength);
        TValidation IsEmail(Expression<Func<TTarget, string>> expression);
        TValidation IsUnique(Expression<Func<TTarget, string>> expression);
        TValidation IsUnique(Expression<Func<TTarget, int>> expression);
        TValidation IsCpf(Expression<Func<TTarget, string>> expression);
        TValidation IsCnpj(Expression<Func<TTarget, string>> expression);
        TValidation IsBetween(Expression<Func<TTarget, int>> expression, int initial, int final);
        TValidation HasCustomValidation(Func<TTarget, bool> expression, string property, string message);
    }
}
