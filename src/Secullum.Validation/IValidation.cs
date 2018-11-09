using System;
using System.Linq.Expressions;

namespace Secullum.Validation
{
    public interface IValidation<TTarget, TValidation>
        where TTarget : class
        where TValidation : IValidation<TTarget, TValidation>
    {
        TValidation HasDisplayText(Expression<Func<TTarget, string>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, int>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, int?>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, float>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, float?>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, DateTime>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, DateTime?>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, Guid>> expression, string displayText);
        TValidation HasDisplayText(Expression<Func<TTarget, Guid?>> expression, string displayText);
        TValidation IsRequired(Expression<Func<TTarget, string>> expression);
        TValidation IsRequired(Expression<Func<TTarget, int>> expression);
        TValidation IsRequired(Expression<Func<TTarget, int?>> expression);
        TValidation IsRequired(Expression<Func<TTarget, DateTime>> expression);
        TValidation IsRequired(Expression<Func<TTarget, DateTime?>> expression);
        TValidation IsRequired(Expression<Func<TTarget, Guid>> expression);
        TValidation IsRequired(Expression<Func<TTarget, Guid?>> expression);
        TValidation HasMaxLength(Expression<Func<TTarget, string>> expression, int maxLength);
        TValidation IsEmail(Expression<Func<TTarget, string>> expression);
        TValidation IsUnique(Expression<Func<TTarget, string>> expression);
        TValidation IsUnique(Expression<Func<TTarget, int>> expression);
        TValidation IsUnique(Expression<Func<TTarget, DateTime>> expression);
        TValidation IsCpf(Expression<Func<TTarget, string>> expression);
        TValidation IsCnpj(Expression<Func<TTarget, string>> expression);
        TValidation IsPis(Expression<Func<TTarget, string>> expression);
        TValidation IsSmallDateTime(Expression<Func<TTarget, DateTime>> expression);
        TValidation IsBetween(Expression<Func<TTarget, int>> expression, int initial, int final);
        TValidation IsBetween(Expression<Func<TTarget, int?>> expression, int initial, int final);
        TValidation IsBetween(Expression<Func<TTarget, float>> expression, float initial, float final);
        TValidation IsBetween(Expression<Func<TTarget, float?>> expression, float initial, float final);
        TValidation IsHour(Expression<Func<TTarget, string>> expression);
        TValidation IsTimespan(Expression<Func<TTarget, string>> expression);
        TValidation Matches(Expression<Func<TTarget, string>> expression, string pattern, string message);
        TValidation HasCustomValidation(Func<TTarget, bool> expression, string property, string message);
    }
}
