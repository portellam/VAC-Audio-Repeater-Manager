namespace VACARM.Domain.Models
{
  public interface IDomainModel
  {
    #region Parameters

    /// <summary>
    /// Primary key
    /// </summary>
    uint Id { get; set; }

    #endregion
  }
}