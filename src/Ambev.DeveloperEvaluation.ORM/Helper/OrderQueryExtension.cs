using FluentValidation;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Helpers;
public static class OrderQueryExtension
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
}
