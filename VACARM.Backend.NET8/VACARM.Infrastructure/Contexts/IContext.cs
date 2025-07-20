using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Contexts
{
  public interface IContext
  {
    #region Parameters

    DbSet<DeviceModel> DeviceDbSet { get; set; }
    DbSet<RepeaterModel> RepeaterDbSet { get; set; }
    DbSet<RepeaterDeviceLinkModel> LinkDbSet { get; set; }

    #endregion
  }
}