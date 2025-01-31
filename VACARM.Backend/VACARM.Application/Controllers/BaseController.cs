using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public class BaseController :
    Controller<BaseModel>,
    IBaseController
  {
    #region Parameters

    private IRepository<BaseModel> baseRepository { get; set; } = 
      new Repository<BaseModel>();

    public IRepository<BaseModel> Repository
    {
      get
      {
        return baseRepository;
      }
      set
      {
        baseRepository = value;
        OnPropertyChanged(nameof(baseRepository));
      }
    }

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseController()
    { 
    }

    public void Restart(uint id)
    {

    }

    #endregion
  }
}
