using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IBaseService<TDbSet, TBaseModel>
    where TDbSet :
      DbSet<TBaseModel>
    where TBaseModel :
      BaseModel
  {
    #region Logic

    /// <summary>
    /// Validate a <typeparamref name="BaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    Task<bool> ValidateAsync(int id);

    #endregion
  }
}