using System.ComponentModel;

namespace VACARM.Domain.Models
{
  public interface IBaseModel
  {
    #region Parameters

    /// <summary>
    /// Primary key
    /// </summary>
    uint Id { get; set; }

    event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}