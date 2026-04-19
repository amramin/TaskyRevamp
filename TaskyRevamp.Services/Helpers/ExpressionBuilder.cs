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

			bool hasDateSeparator = searchText.Contains('/') || searchText.Contains('-');
			bool isDateLike = hasDateSeparator && searchText.All(c => char.IsDigit(c) || c == '/' || c == '-');
			bool isNumericOnly = searchText.All(char.IsDigit) && !hasDateSeparator; 
			foreach (var property in properties)
			{
				// Get proper member access (handles nested paths like x.CreatedBy.NameEnglish)
				var member = GetMemberExpression(property.Body, parameter);
				if (member == null)
					continue;

				var propertyType = Nullable.GetUnderlyingType(member.Type) ?? member.Type;
				bool isDateProperty = propertyType == typeof(DateTime) || propertyType == typeof(DateTime?);
				bool isNumericProperty = propertyType.IsPrimitive || propertyType == typeof(decimal);

				if (isDateLike && !isDateProperty)
					continue;

				if (!isDateLike && !isNumericOnly && (isDateProperty || isNumericProperty))
					continue;

				if (isNumericOnly && isDateProperty )
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

				var cleaned = searchText.Trim();
				var separators = new[] { "/", "-" };
				var parts = cleaned.Split(separators, StringSplitOptions.RemoveEmptyEntries);

				if (!parts.All(p => int.TryParse(p, out _)))
					return null;

				Expression finalExpr = null;
				Expression IntStartsWith(Expression prop, string part)
				{
					var toStringCall = Expression.Call(prop, nameof(int.ToString), Type.EmptyTypes);
					return Expression.Call(toStringCall, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(part));
				}

				// Check if it looks like a full date
				if (parts.Length == 3 && parts[2].Length == 4 && int.TryParse(parts[0].TrimStart('0'), out int d) &&
						int.TryParse(parts[1].TrimStart('0'), out int m) && int.TryParse(parts[2], out int y))
				{
					// Exact match
					finalExpr = Expression.AndAlso(
						Expression.AndAlso(Expression.Equal(dayProp, Expression.Constant(d)), Expression.Equal(monthProp, Expression.Constant(m))),
						Expression.Equal(yearProp, Expression.Constant(y))
					);
					return finalExpr;
				}
				// Single part input
				if (parts.Length == 1)
				{
					var part = parts[0].TrimStart('0');
					if (!int.TryParse(part, out int num))
						return null;

					if (part.Length == 4)
						return Expression.Equal(yearProp, Expression.Constant(num));

					if (part.Length == 3)
						return IntStartsWith(yearProp, part);

					if (part.Length <= 2)
					{
						var dayExpr = Expression.Equal(dayProp, Expression.Constant(num));
						var monthExpr = Expression.Equal(monthProp, Expression.Constant(num));
						return Expression.OrElse(dayExpr, monthExpr);
					}
				}
				// Partial match
				if (parts.Length >= 2 && parts.Length <= 3)
				{
					for (int i = 0; i < parts.Length; i++)
					{
						var part = parts[i].TrimStart('0');
						if (!int.TryParse(part, out _))
							continue;

						Expression partExpr = i switch
						{
							0 => IntStartsWith(dayProp, part),
							1 => IntStartsWith(monthProp, part),
							2 => IntStartsWith(yearProp, part),
							_ => null!
						};

						if (partExpr != null)
							finalExpr = finalExpr == null ? partExpr : Expression.AndAlso(finalExpr, partExpr);
					}
				}
				return finalExpr;
			}

			// Handle Boolean
			if (propertyType == typeof(bool))
			{
				var normalized = searchText.Trim().Replace(" ", "").ToLowerInvariant();
				var currentCulture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
				string[] trueKeywords;
				string[] falseKeywords;
				if (currentCulture == "ar")
				{
					// Arabic keywords only
					trueKeywords = new[] { "نعم", "ن", "1" };
					falseKeywords = new[] { "لا", "ل", "غيرنشط", "0" };
				}
				else
				{
					// English keywords only
					trueKeywords = new[] { "yes", "y", "active", "1" };
					falseKeywords = new[] { "no", "n", "inactive", "0" };
				}

				if (trueKeywords.Any(k => k.StartsWith(normalized) || normalized.StartsWith(k)))
					return Expression.Equal(member, Expression.Constant(true));

				if (falseKeywords.Any(k => k.StartsWith(normalized) || normalized.StartsWith(k)))
					return Expression.Equal(member, Expression.Constant(false));

				return null;
			}

			// Handle numeric types
			if (propertyType.IsPrimitive || propertyType == typeof(decimal))
			{
				if (!searchText.All(char.IsDigit))
					return null;

				var toStr = Expression.Call(member, member.Type.GetMethod(nameof(ToString), Type.EmptyTypes)!);
				var containMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;
				return Expression.Call(toStr, containMethod, Expression.Constant(searchText.Trim()));
			}

			// Handle string or any other type → fallback to string.Contains()
			var toStringMethod = member.Type.GetMethod(nameof(object.ToString), Type.EmptyTypes)!;
			var trimMethod = typeof(string).GetMethod(nameof(string.Trim), Type.EmptyTypes)!;
			var toLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;
			var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;

			var safeToString = Expression.Condition(
				Expression.Equal(member, Expression.Constant(null, member.Type)),
				Expression.Constant(string.Empty),
				Expression.Call(member, toStringMethod)
			);
			var trimmed = Expression.Call(safeToString, trimMethod);
			// Convert any type to string for flexible matching
			//var toStringCall = Expression.Call(member, toStringMethod);
			var left = Expression.Call(trimmed, toLowerMethod);
			var normalizedSearch = (searchText ?? string.Empty).Trim().ToLower();
			var right = Expression.Constant(normalizedSearch.ToLower());

			return Expression.Call(left, containsMethod, right);
		}
	}


}
