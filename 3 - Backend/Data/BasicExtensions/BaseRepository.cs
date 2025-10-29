using Common.Security.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.BasicExtensions
{
   public class BaseRepository
   {

      protected readonly ICurrentUser _currentUser;
      public BaseRepository(ICurrentUser currentUser)
      {
         _currentUser = currentUser;
      }
   }
}
