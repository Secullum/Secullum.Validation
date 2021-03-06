﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using static Secullum.Validation.Localization;
using static Secullum.Validation.Localization.StringTypes;

namespace Secullum.Validation
{
    public class Validation<T> : IValidation<T, Validation<T>> where T : class
    {
        private T target;
        private DbContext dbContext;
        private IDictionary<MemberInfo, string> displayTextDictionary = new Dictionary<MemberInfo, string>();
        private IList<ValidationError> errorList = new List<ValidationError>();

        public Validation(T target) : this(target, null)
        {
        }

        public Validation(T target, DbContext dbContext)
        {
            this.target = target;
            this.dbContext = dbContext;
        }

        public Validation<T> HasDisplayText(Expression<Func<T, string>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        public Validation<T> HasDisplayText(Expression<Func<T, int>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        public Validation<T> HasDisplayText(Expression<Func<T, int?>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        public Validation<T> HasDisplayText(Expression<Func<T, float>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        public Validation<T> HasDisplayText(Expression<Func<T, float?>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        public Validation<T> HasDisplayText(Expression<Func<T, DateTime>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        public Validation<T> HasDisplayText(Expression<Func<T, DateTime?>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        public Validation<T> HasDisplayText(Expression<Func<T, Guid>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        public Validation<T> HasDisplayText(Expression<Func<T, Guid?>> expression, string displayText)
        {
            return HasDisplayText((LambdaExpression)expression, displayText);
        }

        private Validation<T> HasDisplayText(LambdaExpression expression, string displayText)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var memberExpression = (MemberExpression)expression.Body;

            displayTextDictionary.Add(memberExpression.Member, displayText);

            return this;
        }
        
        public Validation<T> IsRequired(Expression<Func<T, string>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (string.IsNullOrWhiteSpace(value))
            {
                AddError((MemberExpression)expression.Body, GetString(IsRequiredMessage));
            }

            return this;
        }

        public Validation<T> IsRequired(Expression<Func<T, int>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (value <= 0)
            {
                AddError((MemberExpression)expression.Body, GetString(IsRequiredMessage));
            }

            return this;
        }

        public Validation<T> IsRequired(Expression<Func<T, int?>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (!value.HasValue)
            {
                AddError((MemberExpression)expression.Body, GetString(IsRequiredMessage));
            }

            return this;
        }

        public Validation<T> IsRequired(Expression<Func<T, DateTime>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (value == default(DateTime))
            {
                AddError((MemberExpression)expression.Body, GetString(IsRequiredMessage));
            }

            return this;
        }

        public Validation<T> IsRequired(Expression<Func<T, DateTime?>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (value == null || value == default(DateTime))
            {
                AddError((MemberExpression)expression.Body, GetString(IsRequiredMessage));
            }

            return this;
        }

        public Validation<T> IsRequired(Expression<Func<T, Guid>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (value == Guid.Empty)
            {
                AddError((MemberExpression)expression.Body, GetString(IsRequiredMessage));
            }

            return this;
        }

        public Validation<T> IsRequired(Expression<Func<T, Guid?>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (!value.HasValue)
            {
                AddError((MemberExpression)expression.Body, GetString(IsRequiredMessage));
            }

            return this;
        }

        public Validation<T> HasMaxLength(Expression<Func<T, string>> expression, int maxLength)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength));
            }

            var value = expression.Compile()(target);
            
            if (value != null && value.Length > maxLength)
            {
                AddError((MemberExpression)expression.Body, GetString(HasMaxLengthMessage), maxLength);
            }
            
            return this;
        }

        public Validation<T> IsEmail(Expression<Func<T, string>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);
            var regex = new Regex(@"^[a-zA-Z0-9_\.-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+$");
            
            if (!string.IsNullOrEmpty(value) && !regex.IsMatch(value))
            {
                AddError((MemberExpression)expression.Body, GetString(IsEmailMessage));
            }

            return this;
        }

        public Validation<T> IsUnique(Expression<Func<T, string>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var propValue = expression.Compile()(target);

            if (string.IsNullOrEmpty(propValue))
            {
                return this;
            }

            return IsUnique(expression, propValue);
        }

        public Validation<T> IsUnique(Expression<Func<T, int>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var propValue = expression.Compile()(target);

            if (propValue == 0)
            {
                return this;
            }

            return IsUnique(expression, propValue);
        }

        public Validation<T> IsUnique(Expression<Func<T, DateTime>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var propValue = expression.Compile()(target);

            if (propValue == null)
            {
                return this;
            }

            return IsUnique(expression, propValue);
        }

        private Validation<T> IsUnique(LambdaExpression expression, object propValue)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            var idProp = typeof(T).GetTypeInfo().GetDeclaredProperty("Id");
            var idValue = idProp.GetValue(target);
            
            // x.Name == "fernando"
            var equalExpression = Expression.Equal(
                expression.Body,
                Expression.Constant(propValue)
            );

            // x.Id != 1
            var idNotEqualExpression = Expression.NotEqual(
                Expression.MakeMemberAccess(expression.Parameters[0], idProp),
                Expression.Constant(idValue)
            );

            // x.Name == "fernando" && x.Id != 1
            var andExpression = Expression.AndAlso(
                equalExpression,
                idNotEqualExpression
            );

            var lambda = Expression.Lambda<Func<T, bool>>(andExpression, expression.Parameters);

            if (dbContext.Set<T>().Any(lambda))
            {
                AddError((MemberExpression)expression.Body, GetString(IsUniqueMessage));
            }
            
            return this;
        }

        public Validation<T> IsCpf(Expression<Func<T, string>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (!string.IsNullOrEmpty(value) && !ValidationUtils.IsCpf(value))
            {
                AddError((MemberExpression)expression.Body, GetString(IsCpfMessage));
            }
            
            return this;
        }

        public Validation<T> IsCnpj(Expression<Func<T, string>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (!string.IsNullOrEmpty(value) && !ValidationUtils.IsCnpj(value))
            {
                AddError((MemberExpression)expression.Body, GetString(IsCnpjMessage));
            }

            return this;
        }

        public Validation<T> IsPis(Expression<Func<T, string>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (!string.IsNullOrEmpty(value) && !ValidationUtils.IsPis(value))
            {
                AddError((MemberExpression)expression.Body, GetString(IsPisMessage));
            }

            return this;
        }

        public Validation<T> IsBetween(Expression<Func<T, int>> expression, int initial, int final)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            return CheckIsBetween(expression, value, initial, final);
        }

        public Validation<T> IsBetween(Expression<Func<T, int?>> expression, int initial, int final)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (value == null)
            {
                return this;
            }

            return CheckIsBetween(expression, value.Value, initial, final);
        }

        public Validation<T> IsBetween(Expression<Func<T, float>> expression, float initial, float final)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            return CheckIsBetween(expression, value, initial, final);
        }

        public Validation<T> IsBetween(Expression<Func<T, float?>> expression, float initial, float final)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (value == null)
            {
                return this;
            }

            return CheckIsBetween(expression, value.Value, initial, final);
        }

        public Validation<T> IsCep(Expression<Func<T, string>> expression)
        {
            var regexCep = @"^\d{5}\-?\d{3}$";

            return Matches(expression, regexCep, GetString(IsHourMessage));
        }

        public Validation<T> IsHour(Expression<Func<T, string>> expression)
        {
            var regexHour = @"^([01][0-9]|2[0-3]):([0-5][0-9])$";

            return Matches(expression, regexHour, GetString(IsHourMessage));
        }

        public Validation<T> IsTimespan(Expression<Func<T, string>> expression)
        {
            var regexTimespan = @"^([0-9]+):([0-5][0-9])$";

            return Matches(expression, regexTimespan, GetString(IsTimespanMessage));
        }

        public Validation<T> Matches(Expression<Func<T, string>> expression, string pattern, string message)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);
            var regex = new Regex(pattern);

            if (!string.IsNullOrEmpty(value) && !regex.IsMatch(value))
            {
                AddError((MemberExpression)expression.Body, message);
            }

            return this;
        }

        public Validation<T> HasCustomValidation(Func<T, bool> expression, string property, string message)
        {
            if (!expression(target))
            {
                errorList.Add(new ValidationError(property, message));
            }

            return this;
        }

        public IList<ValidationError> ToList()
        {
            return new ReadOnlyCollection<ValidationError>(errorList);
        }

        public Validation<T> IsSmallDateTime(Expression<Func<T, DateTime>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            return CheckSmallDateTime(expression, value);
        }

        public Validation<T> IsSmallDateTime(Expression<Func<T, DateTime?>> expression)
        {
            ThrowIfNotMemberAccessExpression(expression.Body);

            var value = expression.Compile()(target);

            if (value == null)
            {
                return this;
            }

            return CheckSmallDateTime(expression, value.Value);
        }

        private Validation<T> CheckSmallDateTime(LambdaExpression expression, DateTime value)
        {
            if (value < new DateTime(1900, 1, 1) || value > new DateTime(2079, 6, 6))
            {
                AddError((MemberExpression)expression.Body, GetString(IsOutOfDate));
            }

            return this;
        }

        private Validation<T> CheckIsBetween(LambdaExpression expression, float value, float initial, float final)
        {
            if (value < initial || value > final)
            {
                AddError((MemberExpression)expression.Body, GetString(IsOutOfRange), initial, final);
            }

            return this;
        }

        private void AddError(MemberExpression expression, string message, params object[] formatArgs)
        {
            var propertyName = expression.Member.Name;
            var propertyDisplayText = "";
            var formatArgsList = new List<object>();

            displayTextDictionary.TryGetValue(expression.Member, out propertyDisplayText);
            
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
