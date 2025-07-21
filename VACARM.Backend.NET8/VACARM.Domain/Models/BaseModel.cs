using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Domain.Models
{
  /// <summary>
  /// Object with primary key.
  /// </summary>
  public class BaseModel :
    IBaseModel
  {
    #region Parameters

    [Required]
    public virtual int Id { get; set; }

    public virtual DateTime CreatedDateTime { get; private set; }
    public virtual DateTime ModifiedDateTime { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public BaseModel()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>

    [ExcludeFromCodeCoverage]
    public BaseModel(int id)
    {
      this.Id = id;
      this.CreatedDateTime = DateTime.UtcNow;
      this.ModifiedDateTime = CreatedDateTime;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="createdDateTime">The construct date-time</param>
    /// <param name="modifiedDateTime">The last date-time a change occurred</param>
    [ExcludeFromCodeCoverage]
    public BaseModel
    (
      int id,
      DateTime createdDateTime,
      DateTime modifiedDateTime
    )
    {
      this.Id = id;
      this.CreatedDateTime = createdDateTime;
      this.ModifiedDateTime = modifiedDateTime;
    }

    [ExcludeFromCodeCoverage]
    public virtual void Deconstruct
    (
      out int id,
      out DateTime createdDateTime,
      out DateTime modifiedDateTime
    )
    {
      id = this.Id;
      createdDateTime = this.CreatedDateTime;
      modifiedDateTime = this.ModifiedDateTime;
    }

    #endregion
  }
}