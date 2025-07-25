using Microsoft.EntityFrameworkCore;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial class Service
  {
    #region Parameters

    protected new IQueryable<RepeaterDeviceLinkModel?> Queryable
    {
      get
      {
        var queryable = base.Queryable;

        queryable = queryable.Include
          (x => (x as RepeaterDeviceLinkModel).InputDeviceId);

        queryable = queryable.Include
          (x => (x as RepeaterDeviceLinkModel).OutputDeviceId);

        queryable = queryable.Include
          (x => (x as RepeaterDeviceLinkModel).RepeaterId);

        return queryable;
      }
    }

    #endregion

    #region Logic

    protected virtual bool Validate(RepeaterDeviceLinkModel model)
    {
      if (!base.Validate(model))
      {
        return false;
      }

      if ((model as RepeaterDeviceLinkModel).InputDeviceId < MinId)
      {
        return false;
      }

      if ((model as RepeaterDeviceLinkModel).OutputDeviceId < MinId)
      {
        return false;
      }

      if ((model as RepeaterDeviceLinkModel).RepeaterId < MinId)
      {
        return false;
      }

      return true;
    }

    public override async Task<bool> ValidateAsync(int id)
    {
      var link = await GetAsync(id);
      var result = Validate(link);
      return result;
    }

    public async Task<RepeaterDeviceLinkModel?> GetAsyncByDeviceId(int deviceId)
    {
      var func = new Func<RepeaterDeviceLinkModel?, bool>
        (
          x =>
          {
            return x.InputDeviceId == deviceId
              || x.OutputDeviceId == deviceId;
          }
        );

      return await GetAsync(func);
    }

    public async Task<RepeaterDeviceLinkModel?> GetAsyncByRepeaterId(int repeaterId)
    {
      var func = new Func<RepeaterDeviceLinkModel?, bool>
        (x => x.RepeaterId == repeaterId);

      return await GetAsync(func);
    }

    #endregion
  }
}