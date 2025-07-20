using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Contexts
{
  public partial class Context
  {
    #region Parameters

    public DbSet<DeviceModel> DeviceDbSet { get; set; }
    public DbSet<RepeaterModel> RepeaterDbSet { get; set; }
    public DbSet<RepeaterDeviceLinkModel> LinkDbSet { get; set; }

    #endregion
  }
}