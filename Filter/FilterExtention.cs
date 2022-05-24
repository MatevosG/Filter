using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    public static class FilterExtention
    {
        public static IQueryable<T> TextFilter<T>(this IQueryable<T> source, string term)
        {
            if (string.IsNullOrEmpty(term)) { return source; }

            Type elementType = typeof(T);

            PropertyInfo[] stringProperties =
                elementType.GetProperties()
                    .Where(x => x.PropertyType == typeof(string))
                    .ToArray();
            if (!stringProperties.Any()) { return source; }

            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            ParameterExpression prm = Expression.Parameter(elementType);

            IEnumerable<Expression> expressions = stringProperties
                .Select(prp =>
                   Expression.Call(
                      Expression.Property(
                            prm,
                            prp
                        ),
                        containsMethod,
                      Expression.Constant(term)
                    )
                );

            Expression body = expressions
                .Aggregate(
                    (prev, current) => Expression.Or(prev, current)
                );

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(body, prm);

            return source.Where(lambda);
        }
    }
}
