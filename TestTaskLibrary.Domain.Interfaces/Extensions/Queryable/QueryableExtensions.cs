using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TestTaskLibrary.Domain.Application.Extensions.Queryable
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> Search<TSource>(this IQueryable<TSource> source, string property, string search)
        {
            var arg = Expression.Parameter(typeof(TSource), "i");

            Expression expression = Expression.Property(arg, property);

            //expression = Expression.Call(expression, "ToString",null);

            expression = Expression.Call(expression, "ToLower", null);

            Expression searchVal = Expression.Constant(search.ToLower());

            expression = Expression.Call(expression, "Contains", null, searchVal);

            var lambda = Expression.Lambda(expression, arg);

            var methods = typeof(System.Linq.Queryable).GetMethods().Where(m => m.Name == "Where");
            var method = methods.FirstOrDefault(m => m.GetParameters().Any(p => p.ParameterType.GenericTypeArguments[0].GenericTypeArguments.Count() == 2)).MakeGenericMethod(typeof(TSource));
            return (IQueryable<TSource>)method.Invoke(null, new object[] { source, lambda });

            //Where(i => i.FullName.ToString().ToLower().Contans(search.ToLower()));

        }

        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string sortField)
        {
            var arg = Expression.Parameter(typeof(TSource), "i");

            var expression = Expression.Property(arg, sortField);
            var lambda = Expression.Lambda(expression, arg);

            var methods = typeof(System.Linq.Queryable).GetMethods().Where(m => m.Name == "OrderBy");
            var method = methods.FirstOrDefault(m => m.GetParameters().Count() == 2).MakeGenericMethod(typeof(TSource), expression.Type);

            return (IOrderedQueryable<TSource>)method.Invoke(null, new object[] { source, lambda });
        }
    }
}
