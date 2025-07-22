using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Contexts;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class Service :
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

    public void DoAction
    (
      Action<IBaseModel> action,
      Func<IBaseModel, bool> func
    )
    {
      throw new NotImplementedException();
    }

    public void DoAction
    (
      Action<IBaseModel> action,
      IBaseModel item
    )
    {
      throw new NotImplementedException();
    }

    public void DoActionAll(Action<IBaseModel> action)
    {
      throw new NotImplementedException();
    }

    public void DoActionRange
    (
      Action<IBaseModel> action,
      IEnumerable<IBaseModel> enumerable
    )
    {
      throw new NotImplementedException();
    }

    public void DoActionRange
    (
      Action<IBaseModel> action,
      Func<IBaseModel, bool> func
    )
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
