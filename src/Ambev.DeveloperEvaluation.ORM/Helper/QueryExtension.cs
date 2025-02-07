using FluentValidation;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Helpers;
public static class QueryExtension
{
    public static IQueryable<T> ApplyOrdering<T>(
        this IQueryable<T> query, List<(string PropertyName, bool Ascendent)> sortingFields)
    {
        if (sortingFields == null || sortingFields.Count == 0)
            return query;

        var param = Expression.Parameter(typeof(T), "x");
        IOrderedQueryable<T> orderedQuery = null!;

        foreach (var (propertyName, ascendent) in sortingFields)
        {
            var propertyInfo = typeof(T).GetProperties().FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase))
                ?? throw new ValidationException($"Property '{propertyName}' does not exist or cannot be used for sorting.");

            var property = Expression.Property(param, propertyName);
            var lambda = Expression.Lambda(property, param);
            string methodName;

            if (orderedQuery == null)
                methodName = ascendent ? "OrderBy" : "OrderByDescending";
            else
                methodName = ascendent ? "ThenBy" : "ThenByDescending";

            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            orderedQuery = (IOrderedQueryable<T>)method.Invoke(null, [orderedQuery ?? query, lambda])!;
        }

        return orderedQuery ?? query;
    }

    public static IQueryable<T> FilterStringField<T>(this IQueryable<T> query, string propertyName, object? value)
    {
        const string wildcardCharacter = "*";
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);

        // Check if the value is not null and is a string
        if (value is string valueString && valueString.Contains(wildcardCharacter))
        {
            var searchPattern = valueString.Replace(wildcardCharacter, string.Empty); // Removes the * for comparison
            Expression comparison = null!;

            // Converts property and search pattern to lower case to perform case-insensitive comparison
            var propertyLower = Expression.Call(property, "ToLower", null);
            var searchPatternLower = Expression.Constant(searchPattern.ToLower());

            // Checks if the * is at the start, at the end, or on both sides
            if (valueString.StartsWith(wildcardCharacter) && valueString.EndsWith(wildcardCharacter))
            {
                comparison = Expression.Call(propertyLower, "Contains", null, searchPatternLower);
            }
            else if (valueString.StartsWith(wildcardCharacter))
            {
                comparison = Expression.Call(propertyLower, "EndsWith", null, searchPatternLower);
            }
            else if (valueString.EndsWith(wildcardCharacter))
            {
                comparison = Expression.Call(propertyLower, "StartsWith", null, searchPatternLower);
            }

            // If there's no *, performs an exact equality comparison
            comparison ??= Expression.Equal(propertyLower, searchPatternLower);

            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
            return query.AsQueryable().Where(lambda);
        }
        else if (value != null)
        {
            // If there's no *, performs an exact equality comparison for other types
            var constant = Expression.Constant(value);
            var propertyLower = Expression.Call(property, "ToLower", null);
            var constantLower = Expression.Constant(value.ToString()?.ToLower());

            var equal = Expression.Equal(propertyLower, constantLower);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            return query.AsQueryable().Where(lambda);
        }
        else
        {
            // If value is null, you may choose to filter by null or return the original query.
            Expression nullCheck = Expression.Equal(property, Expression.Constant(null, property.Type));
            var lambda = Expression.Lambda<Func<T, bool>>(nullCheck, parameter);

            return query.AsQueryable().Where(lambda);
        }
    }

    public static IQueryable<T> FilterRangeField<T>(this IQueryable<T> query, string propertyName, object? value)
    {
        // Check if the property name ends with "_min" or "_max"
        var isMinFilter = propertyName.StartsWith("_min", StringComparison.CurrentCultureIgnoreCase);
        var isMaxFilter = propertyName.StartsWith("_max", StringComparison.CurrentCultureIgnoreCase);

        // Remove the "_min" or "_max" prefix from the property name
        var basePropertyName = isMinFilter || isMaxFilter
            ? propertyName[4..] // Remove "_min" or "_max"
            : propertyName;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, basePropertyName);

        // Check if the value is comparable (numeric, date, etc.)
        if (value is IComparable comparableValue)
        {
            Expression comparison;

            // If it's a "_min" filter, check if the property value is greater than or equal to the min value
            if (isMinFilter)
            {
                comparison = Expression.GreaterThanOrEqual(property, Expression.Constant(comparableValue));
            }
            // If it's a "_max" filter, check if the property value is less than or equal to the max value
            else if (isMaxFilter)
            {
                comparison = Expression.LessThanOrEqual(property, Expression.Constant(comparableValue));
            }
            else
            {
                // If it's not a range filter, perform an exact equality comparison
                comparison = Expression.Equal(property, Expression.Constant(value));
            }

            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
            return query.AsQueryable().Where(lambda);
        }
        else
        {
            // If the value is not comparable, throw an exception or handle it differently
            throw new ArgumentException("The value must be a comparable type.");
        }
    }
}
