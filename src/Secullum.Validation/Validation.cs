using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using static Secullum.Validation.Localization;
using static Secullum.Validation.Localization.StringTypes;

namespace Secullum.Validation
{
    public class Validation<T> where T : class
    {
        private T target;
        private IList<ValidationError> errorList = new List<ValidationError>();

        public Validation(T target)
        {
            this.target = target;
        }

        public Validation<T> IsRequired(Expression<Func<T, string>> expression, string propertyDisplayText = null)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (string.IsNullOrWhiteSpace(value))
            {
                propertyDisplayText = propertyDisplayText ?? ((MemberExpression)expression.Body).Member.Name;

                AddError((MemberExpression)expression.Body, propertyDisplayText, GetString(IsRequiredMessage));
            }

            return this;
        }

        public Validation<T> HasMaxLength(Expression<Func<T, string>> expression, int maxLength, string propertyDisplayText = null)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength));
            }

            var value = expression.Compile()(target);

            if (value != null && value.Length > maxLength)
            {
                AddError((MemberExpression)expression.Body, propertyDisplayText, GetString(HasMaxLengthMessage), maxLength);
            }
            
            return this;
        }

        public Validation<T> IsEmail(Expression<Func<T, string>> expression, string propertyDisplayText = null)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);
            var regex = new Regex(@"^[a-zA-Z0-9_\.-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+$");
            
            if (!string.IsNullOrEmpty(value) && !regex.IsMatch(value))
            {
                AddError((MemberExpression)expression.Body, propertyDisplayText, GetString(IsEmailMessage));
            }

            return this;
        }

        public IList<ValidationError> ToList()
        {
            return new ReadOnlyCollection<ValidationError>(errorList);
        }

        private void AddError(MemberExpression expression, string propertyDisplayText, string message, params object[] formatArgs)
        {
            var propertyName = expression.Member.Name;
            var formatArgsList = new List<object>();
            
            formatArgsList.Add(propertyDisplayText ?? propertyName);
            formatArgsList.AddRange(formatArgs);
            
            errorList.Add(new ValidationError(propertyName, string.Format(message, formatArgsList.ToArray())));
        }

        private void ThrowIfNotMemberAccessExpression(Expression expression)
        {
            if (expression.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException(GetString(InvalidExpressionMessage), nameof(expression));
            }
        }
    }
}
