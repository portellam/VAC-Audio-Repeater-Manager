#warning Differs from projects of earlier NET revisions (below v4.6).

using System;
using System.Collections.Generic;
using System.Linq;

namespace VACARM.Extensions
{
  public class ArrayExtension<T>
  {
    #region Parameters

    public static T[] Empty<T>()
    {
      

      return Array.Empty<T>();
    }

    #endregion
  }
}