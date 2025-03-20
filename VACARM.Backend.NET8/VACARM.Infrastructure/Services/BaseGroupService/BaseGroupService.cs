using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services.BaseGroupService;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The service of <typeparamref name="TBaseModel"/> service(s).
  /// </summary>
  public partial class BaseGroupService
    <
      TBaseService,
      TBaseModel
    > :
    BaseRepository<TBaseService>,
    IBaseGroupService
    <
      TBaseService,
      TBaseModel
    >
    where TBaseService :
    BaseService<TBaseModel>,
    new()
    where TBaseModel :
    class,
    IBaseModel,
    new()
  {
    #region Parameters

    private uint selectedId { get; set; } = MinCount;

    public BaseRepository<TBaseModel> SelectedRepository
    {
      get
      {
        return this.SelectedService
          .Repository;
      }
      protected set
      {
        this.SelectedService
          .Repository = value;

        base.OnPropertyChanged(nameof(this.SelectedRepository));
      }
    }

    public BaseService<TBaseModel> SelectedService
    {
      get
      {
        return base.Get(this.SelectedId);
      }
      protected set
      {
        var service = value;
        service.Id = this.SelectedId;
        this.Update(service);
        base.OnPropertyChanged(nameof(this.SelectedService));
      }
    }

    public uint SelectedId
    {
      get
      {
        return this.selectedId;
      }
      set
      {
        if (value < MinCount)
        {
          value = MinCount;
        }

        this.selectedId = value;
        base.OnPropertyChanged(nameof(this.SelectedId));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public BaseGroupService(int maxCount) :
      base()
    {
      this.Add(base.EmptyModel);
      this.MaxCount = maxCount;
    }

    public async void Export
    (
      uint id,
      string filePathName = null
    )
    {
      if (!base.IsValidId(id))
      {
        return;
      }

      var service = this.Get(id);

      if (service == null)
      {
        Debug.WriteLine("Failed to export. Service is not valid.");
        return;
      }

      if (string.IsNullOrWhiteSpace(service.FilePathName))
      {
        if (string.IsNullOrWhiteSpace(filePathName))
        {
          Debug.WriteLine("Failed to export. File name is not valid.");
          return;
        }

        service.FilePathName = filePathName;
      }

      await service.WriteAllToFile();
    }

    public async void Import
    (
      uint id,
      string filePathName = null
    )
    {
      if (!base.IsValidId(id))
      {
        return;
      }

      var service = this.Get(id);

      if (string.IsNullOrWhiteSpace(service.FilePathName))
      {
        if (string.IsNullOrWhiteSpace(filePathName))
        {
          Debug.WriteLine("Failed to import. File name is not valid.");
          return;
        }

        service.FilePathName = filePathName;
      }

      await service.ReadRangeFromFile();

      if (service == null)
      {
        Debug.WriteLine("Failed to import. Service is not valid.");
        return;
      }

      this.Update(service);
    }

    #endregion
  }
}