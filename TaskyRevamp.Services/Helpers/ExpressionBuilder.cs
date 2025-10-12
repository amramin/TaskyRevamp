using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models;

namespace TaskyRevamp.Services.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ExpressionBuilder
    {
        public static Expression<Func<TEntity, bool>> BuildLikeExpression<TEntity>(
            IEnumerable<Expression<Func<TEntity, object>>> properties, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return x => true;

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression? body = null;

            foreach (var property in properties)
            {
                // Get proper member access (handles nested paths like x.CreatedBy.NameEnglish)
                var member = GetMemberExpression(property.Body, parameter);
                if (member == null)
                    continue;

                var comparison = BuildComparisonExpression(member, searchText);
                if (comparison != null)
                    body = body == null ? comparison : Expression.OrElse(body, comparison);
            }

            return Expression.Lambda<Func<TEntity, bool>>(body ?? Expression.Constant(true), parameter);
        }

        private static MemberExpression? GetMemberExpression(Expression body, ParameterExpression parameter)
        {
            // Handle conversions (e.g. boxing value types to object)
            if (body is UnaryExpression unary && unary.NodeType == ExpressionType.Convert)
                body = unary.Operand;

            if (body is MemberExpression member)
            {
                if (member.Expression is ParameterExpression)
                    return Expression.MakeMemberAccess(parameter, member.Member);

                var inner = GetMemberExpression(member.Expression!, parameter);
                return Expression.MakeMemberAccess(inner!, member.Member);
            }

            return null;
        }

        private static Expression? BuildComparisonExpression(MemberExpression member, string searchText)
        {
            var propertyType = Nullable.GetUnderlyingType(member.Type) ?? member.Type;

            // Handle DateTime and DateTime?
            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                // Try to parse the date using multiple common formats and cultures
                var acceptedFormats = new[] { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "d/M/yyyy" };

                DateTime searchDate;

                bool parsed = DateTime.TryParseExact(searchText,acceptedFormats,CultureInfo.InvariantCulture,DateTimeStyles.None, out searchDate)
                            ||
                            DateTime.TryParse(searchText, CultureInfo.CurrentCulture, DateTimeStyles.None, out searchDate);

                if (parsed)
                {
                    // If property is nullable, access its Value
                    Expression dateValue = member;
                    if (member.Type == typeof(DateTime?))
                    {
                        dateValue = Expression.Property(member, "Value");
                    }

                    // Compare by date only (ignore time)
                    var dateProperty = Expression.Property(dateValue, nameof(DateTime.Date));
                    var constant = Expression.Constant(searchDate.Date, typeof(DateTime));

                    return Expression.Equal(dateProperty, constant);
                }

                return null;
            }

            // Handle Boolean
            if (propertyType == typeof(bool))
            {
                if (bool.TryParse(searchText, out var boolValue))
                    return Expression.Equal(member, Expression.Constant(boolValue));

                if (searchText.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                    searchText.Equals("active", StringComparison.OrdinalIgnoreCase) ||
                    searchText == "1")
                    return Expression.Equal(member, Expression.Constant(true));

                if (searchText.Equals("no", StringComparison.OrdinalIgnoreCase) ||
                    searchText.Equals("inactive", StringComparison.OrdinalIgnoreCase) ||
                    searchText == "0")
                    return Expression.Equal(member, Expression.Constant(false));

                return null;
            }

            // Handle numeric types
            if (propertyType.IsPrimitive || propertyType == typeof(decimal))
            {
                if (decimal.TryParse(searchText, out var num))
                {
                    var converted = Expression.Convert(member, typeof(decimal));
                    var constant = Expression.Constant(num);
                    return Expression.Equal(converted, constant);
                }
                return null;
            }

            // Handle string or any other type → fallback to string.Contains()
            var toStringMethod = member.Type.GetMethod(nameof(object.ToString), Type.EmptyTypes)!;
            var toLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;
            var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;

            // Convert any type to string for flexible matching
            var toStringCall = Expression.Call(member, toStringMethod);
            var left = Expression.Call(toStringCall, toLowerMethod);
            var right = Expression.Constant(searchText.ToLower());

            return Expression.Call(left, containsMethod, right);
        }
    }


}
