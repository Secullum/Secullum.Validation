using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public Validation<T> IsRequired(Expression<Func<T, string>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (string.IsNullOrWhiteSpace(value))
            {
                AddError((MemberExpression)expression.Body, "O campo {0} é obrigatório.");
            }

            return this;
        }

        public IList<ValidationError> ToList()
        {
            return errorList;
        }

        private void AddError(MemberExpression expression, string message)
        {
            var propertyName = expression.Member.Name;

            errorList.Add(new ValidationError(propertyName, string.Format(message, propertyName)));
        }

        private void ThrowIfNotMemberAccessExpression(Expression expression)
        {
            if (expression.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Expressão inválida", nameof(expression));
            }
        }
    }
}
