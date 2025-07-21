using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VACARM.Domain.Models
{
  public interface IBaseModel
  {
    #region Parameters

    /// <summary>
    /// Primary key
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// The construct date-time.
    /// </summary>
    DateTime CreatedDateTime { get; }

    /// <summary>
    /// The last date-time a change occurred.
    /// </summary>
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
      out int id,
      out DateTime createdDateTime,
      out DateTime modifiedDateTime
    );

    #endregion
  }
}