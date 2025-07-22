using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

    /// <summary>
    /// Do an action for a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoAction
    (
      Action<IBaseModel> action,
      Func<IBaseModel, bool> func
    );

    /// <summary>
    /// Do an action for a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="item">The item</param>
    void DoAction
    (
      Action<IBaseModel> action,
      IBaseModel item
    );

    /// <summary>
    /// Do an action for the enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="action">The action</param>
    void DoActionAll(Action<IBaseModel> action);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void DoActionRange
    (
      Action<IBaseModel> action,
      IEnumerable<IBaseModel> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoActionRange
    (
      Action<IBaseModel> action,
      Func<IBaseModel, bool> func
    );

    #endregion
  }
}
