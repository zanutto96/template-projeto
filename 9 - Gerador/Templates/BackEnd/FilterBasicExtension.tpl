using Domain.Entities;
using Domain.Filters; 
using System;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq.Dynamic.Core;


namespace Data.BasicExtensions
{
    public static class #Entity#FilterBasicExtension
    {
        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().Single(method =>
           method.Name == "OrderBy" && method.GetParameters().Length == 2);
        public static IQueryable<#Entity#> WithBasicFilters(this IQueryable<#Entity#> queryBase, #Entity#Filter filters)
        {
            var queryFilter = queryBase;


            #EntityBasicFilters#


            return queryFilter;
        }

        public static IQueryable<#Entity#> WithCustomFilters(this IQueryable<#Entity#> queryBase, #Entity#Filter filters)
        {
            var queryFilter = queryBase;



            return queryFilter;
        }

        public static IQueryable<#Entity#> OrderByDomain(this IQueryable<#Entity#> queryBase, #Entity#Filter filters)
        {
           if (!string.IsNullOrEmpty(filters.OrderBy))
            {
               if (filters.OrderByType == OrderByType.OrderByDescending)
                  return queryBase.AsQueryable().OrderBy(filters.OrderBy + " DESC");
               else if (filters.OrderByType == OrderByType.OrderBy)
                  return queryBase.AsQueryable().OrderBy(filters.OrderBy + " ASC");
            }
            return queryBase;
        }

        

        public static IQueryable<T> OrderByPropertyAscending<T>(this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return source;
            }
            var paramterExpression = Expression.Parameter(typeof(T));
            var orderByProperty = Expression.Property(paramterExpression, propertyName);
            var lambda = Expression.Lambda(orderByProperty, paramterExpression);
            var genericMethod = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            var ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

        private static string DefinePropertyName(string[] propertyName)
        {
            var _propertyName = propertyName.LastOrDefault();
            var _parentProperty = _propertyName.Split('.')[0];
            return _parentProperty;
        }
    }
}
