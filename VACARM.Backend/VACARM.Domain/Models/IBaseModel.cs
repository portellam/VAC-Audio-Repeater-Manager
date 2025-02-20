using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VACARM.Domain.Models
{
  public interface IBaseModel
  {
    #region Parameters

    /// <summary>
    /// Primary key
    /// </summary>
    [Required]
    uint Id { get; set; }

    event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}