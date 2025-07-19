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

    /// <summary>
    /// The construct date-time.
    /// </summary>
    [Required]
    DateTime CreatedDateTime { get; }

    /// <summary>
    /// The last date-time a change occurred.
    /// </summary>
    [Required]
    DateTime ModifiedDateTime { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Deconstructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="createdDateTime">The construct date-time</param>
    /// <param name="modifiedDateTime">The last date-time a change occurred</param>
    void Deconstruct
    (
      out uint id,
      out DateTime createdDateTime,
      out DateTime modifiedDateTime
    );

    #endregion
  }
}