using System;
using System.Linq;
using VACARM.Extensions;

namespace VACARM.Common
{
  public partial class Info
  {
    #region Parameters

    public static string[] FilePathNames { get; set; } = 
      ArrayExtension<string>.EmptyArray;
    
    #endregion
  }
}