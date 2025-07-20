using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VACARM.Infrastructure.Contexts
{
  public partial class Context :
    DbContext,
    IContext
  {
    #region Logic

    protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
    {
      dbModelBuilder.Conventions
        .Remove<PluralizingTableNameConvention>();
    }

    #endregion
  }
}