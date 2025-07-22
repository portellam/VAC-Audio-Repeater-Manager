using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Contexts;

namespace VACARM.Infrastructure.NewServices
{
  public class Service :
    IService
  {
    #region Parameters

    private Context Context { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">The context</param>
    public Service(Context context)
    {
      this.Context = context;
    }

    public async Task<IBaseModel> CreateAsync()
    {
      // Validate input/output devices
      // Create RepeaterDeviceLink
      // Save changes
      throw new NotImplementedException();
    }

    public async Task<IBaseModel> RemoveAsync()
    {
      throw new NotImplementedException();
    }

    public async Task<IBaseModel> ValidateAsync()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
