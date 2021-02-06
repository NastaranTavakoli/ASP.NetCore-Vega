using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;

namespace Persistence.Extensions
{
    public static class IQueryableExtensions
    {

        public static IQueryable<Vehicle> ApplyFiltering(this IQueryable<Vehicle> query, VehicleQuery queryObject)
        {
            if (queryObject.MakeId != null)
            {
                query = query.Where(v => v.Model.MakeId == queryObject.MakeId);
            }
            if (queryObject.ModelId != null)
            {
                query = query.Where(v => v.ModelId == queryObject.ModelId);
            }
            return query;
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Func<T, object>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObject.SortBy) || !columnsMap.ContainsKey(queryObject.SortBy))
            {
                return query;
            }

            if (queryObject.IsSortAscending)
            {
                return query.OrderBy(columnsMap[queryObject.SortBy]).AsQueryable();
            }
            else
            {
                return query.OrderByDescending(columnsMap[queryObject.SortBy]).AsQueryable();
            }

        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObject)
        {
            if (queryObject.PageSize <= 0)
            {
                queryObject.PageSize = 10;
            }
            if (queryObject.Page <= 0)
            {
                queryObject.Page = 1;
            }
            return query.Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
        }
    }
}