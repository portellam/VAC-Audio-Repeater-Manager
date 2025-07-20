using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VACARM.Infrastructure.Contexts
{
  public partial class Context :
    DbContext,
    IContext
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public Context() :
      base(nameof(Context))
    {
    }

    #endregion
  }
}