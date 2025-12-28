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
				Expression dateValue = member;
				if (member.Type == typeof(DateTime?))
					dateValue = Expression.Property(member, "Value");
				var dayProp = Expression.Property(dateValue, nameof(DateTime.Day));
				var monthProp = Expression.Property(dateValue, nameof(DateTime.Month));
				var yearProp = Expression.Property(dateValue, nameof(DateTime.Year));
				// Normalize input
				var cleaned = searchText.Trim().Replace(" ", "");

				// Year-only search (4 digits)
				if (cleaned.Length == 4 && int.TryParse(cleaned, out int year))
					return Expression.Equal(yearProp, Expression.Constant(year));

				// Day or Month-only search (1–2 digits)
				if (cleaned.Length <= 2 && int.TryParse(cleaned, out int num))
				{
					Expression dayMatch = Expression.Equal(dayProp, Expression.Constant(num));
					Expression monthMatch = Expression.Equal(monthProp, Expression.Constant(num));
					return Expression.OrElse(dayMatch, monthMatch);
				}

				// Month + Year (e.g. 11-2025 or 11/2025)
				var parts = cleaned.Split('/', '-', '.');
				if (parts.Length == 2 &&
					parts[0].Length <= 2 && parts[1].Length <= 2 &&
					int.TryParse(parts[0], out int d) &&
					int.TryParse(parts[1], out int mm))
				{
					Expression dayMatch = Expression.Equal(dayProp, Expression.Constant(d));
					Expression monthMatch = Expression.Equal(monthProp, Expression.Constant(mm));
					return Expression.AndAlso(dayMatch, monthMatch);
				}
				// day + month (03/11 or 15-06)
				if (parts.Length == 2 &&
					int.TryParse(parts[0], out int m) &&
					int.TryParse(parts[1], out int y))
				{
					var monthMatch = Expression.Equal(monthProp, Expression.Constant(m));
					var yearMatch = Expression.Equal(yearProp, Expression.Constant(y));
					return Expression.AndAlso(monthMatch, yearMatch);
				}

				// full date 
				var acceptedFormats = new[] { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "d/M/yyyy" };
				if (DateTime.TryParseExact(searchText, acceptedFormats, CultureInfo.InvariantCulture,
						DateTimeStyles.None, out var fullDate))
				{
					var dateProp = Expression.Property(dateValue, nameof(DateTime.Date));
					return Expression.Equal(dateProp, Expression.Constant(fullDate.Date));
				}
				return null;
			}

			// Handle Boolean
			if (propertyType == typeof(bool))
			{
				var normalized = searchText.Trim().Replace(" ", "").ToLowerInvariant();
				var trueKeywords = new[]{ "yes", "y", "active", "1", "نعم", "ن"};
				var falseKeywords = new[]{ "no", "n", "inactive", "0", "لا", "ل", "غيرنشط" };
				if (trueKeywords.Any(k => k.StartsWith(normalized) || normalized.StartsWith(k)))
					return Expression.Equal(member, Expression.Constant(true));

				if (falseKeywords.Any(k => k.StartsWith(normalized) || normalized.StartsWith(k)))
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
