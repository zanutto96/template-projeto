using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
   public class FilterBase
   {
      public FilterBase()
      {
         this.PageIndex = 0;
         this.PageSize = 50;
         this.IsPagination = true;
      }

      public int PageSkipped
      {
         get
         {
            return (this.PageIndex.Value > 0 ? this.PageIndex.Value - 1 : 0) * this.PageSize.Value;
         }
      }

      public int? PageIndex { get; set; }
      public int? PageSize { get; set; }
      public bool? IsPagination { get; set; }
      public bool? IsOrderByDomain { get; set; }
      public string?[] OrderFields { get; set; }
      public string OrderBy{ get; set; }
      public OrderByType? OrderByType { get; set; }


   }

   public enum OrderByType
   {
      OrderBy,
      OrderByDescending
   }
}

