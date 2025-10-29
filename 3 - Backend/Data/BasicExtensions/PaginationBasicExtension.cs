using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.BasicExtensions
{
   public static class PaginationBasicExtension
   {
      public static IQueryable<T2> Paging<T2>(this IQueryable<T2> source, FilterBase filter)
      {
         if (filter.IsPagination.Value)
         {
            var pageIndex = filter.PageIndex.Value > 0 ? filter.PageIndex - 1 : 0;
            var pageSize = filter.PageSize.Value;
            var totalPages = (int)Math.Ceiling(source.Count() / (double)pageSize);

            return source.Skip(filter.PageSkipped).Take(pageSize);
         }

         return source;

      }

   }
   public class PagingResult<T>
   {
      public Summary Summary { get; set; }
      public List<T> DataList { get; set; }
   }
   public class  Summary
   {
      public int Total { get; set; }
      public int PageSize { get; set; }
   }
}
