using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.NewServices
{
  public interface IService
  {
    #region Parameters

    #endregion

    #region Logic

    Task<IBaseModel> CreateAsync();
    Task<IBaseModel> RemoveAsync();
    Task<IBaseModel> ValidateAsync();

    #endregion
  }
}
