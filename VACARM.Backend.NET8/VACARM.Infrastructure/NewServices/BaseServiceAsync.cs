using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Contexts;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Services
{
  public partial class BaseService<TDbSet, TBaseModel>
    where TDbSet :
      DbSet<TBaseModel>
    where TBaseModel :
      BaseModel,
    IBaseService<TDbSet, TBaseModel>
  {
    #region Parameters

    private DbSet<TBaseModel> DbSet { get; set; }

    private static readonly int MinId = 0;

    #endregion

    #region Logic

    public async Task<bool> ValidateAsync(int id)
    {
      var type = typeof(TBaseModel);

      var query = this.DbSet
        .AsQueryable();

      if (type == typeof(BaseModel))
      {
        query = query.Include(x => (x as BaseModel).Id);
        query = query.Include(x => (x as BaseModel).CreatedDateTime);

        //TODO: validate DateTime properties?
      }

      switch (type)
      {
        case Type t when t == typeof(DeviceModel):
          query = query.Include(x => (x as DeviceModel).ActualId);
          break;

        case Type t when t == typeof(RepeaterModel):
          query = query.Include(x => (x as RepeaterModel).LinkId);

          break;

        case Type t when t == typeof(RepeaterDeviceLinkModel):
          query = query.Include(x => (x as RepeaterDeviceLinkModel).InputDeviceId);
          query = query.Include(x => (x as RepeaterDeviceLinkModel).OutputDeviceId);
          query = query.Include(x => (x as RepeaterDeviceLinkModel).RepeaterId);
          break;

        case Type t when t != typeof(BaseModel):
          //Invalid type
          //NOTE: this may never happen?
          break;
      }

      var link = await query.FirstOrDefaultAsync(l => l.Id == id);

      if (link == null)
      {
        return false;
      }

      if (link.Id < MinId)
      {
        return false;
      }

      switch (type)
      {
        case Type t when t == typeof(DeviceModel):
          var actualId = (link as DeviceModel).ActualId;

          if (string.IsNullOrWhiteSpace(actualId))
          {
            return false;
          }

          break;

        case Type t when t == typeof(RepeaterModel):
          if ((link as RepeaterModel).LinkId < MinId)
          {
            return false;
          }

          break;

        case Type t when t == typeof(RepeaterDeviceLinkModel):
          if ((link as RepeaterDeviceLinkModel).InputDeviceId < MinId)
          {
            return false;
          }

          if ((link as RepeaterDeviceLinkModel).OutputDeviceId < MinId)
          {
            return false;
          }

          if ((link as RepeaterDeviceLinkModel).RepeaterId < MinId)
          {
            return false;
          }

          break;

        case Type t when t != typeof(BaseModel):
          //Invalid type
          break;
      }

      return true;
    }

    #endregion
  }
}