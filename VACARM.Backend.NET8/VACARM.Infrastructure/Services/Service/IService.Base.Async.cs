using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Parameters

    int MinId { get; set; }

    #endregion
  }
}