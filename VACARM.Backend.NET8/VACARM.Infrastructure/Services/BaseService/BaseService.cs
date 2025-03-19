﻿using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The repository for <typeparamref name="TBaseModel"/>.
  /// </summary>
  /// <typeparam name="TBaseModel"></typeparam>
  public partial class BaseService<TBaseModel> :
    Service
    <
      ObservableCollection<TBaseModel>,
      TBaseModel
    >,
    IBaseService<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    private BaseRepository<TBaseModel> repository { get; set; }

    public override Repository
      <
        ObservableCollection<TBaseModel>,
        TBaseModel
      > Repository
    {
      get
      {
        return this.repository;
      }
      set
      {
        this.repository = (BaseRepository<TBaseModel>)value;
        base.OnPropertyChanged(nameof(this.Repository));
      }
    }

    public string FilePathName { get; set; } = string.Empty;

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="filePathName">The file path name</param>
    [ExcludeFromCodeCoverage]
    public BaseService(string filePathName = null) :
      base(new ObservableCollection<TBaseModel>())
    {
      if (string.IsNullOrWhiteSpace(filePathName))
      {
        filePathName = string.Empty;
      }

      this.FilePathName = filePathName;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <param name="filePathName">The file path name</param>
    [ExcludeFromCodeCoverage]
    public BaseService
    (
      BaseRepository<TBaseModel> repository,
      string filePathName = null
    ) :
      base(repository)
    {
      if (string.IsNullOrWhiteSpace(filePathName))
      {
        filePathName = string.Empty;
      }

      this.FilePathName = filePathName;
    }

    #endregion
  }
}