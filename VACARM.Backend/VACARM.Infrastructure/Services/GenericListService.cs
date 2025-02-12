﻿using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class GenericListService<TRepository, TItem> :
    GenericService<GenericListRepository<TItem>, TItem>,
    IGenericListService<GenericListRepository<TItem>, TItem> where TRepository :
    GenericListRepository<TItem> where TItem :
    class
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public GenericListService()
    {
      base._Repository = new GenericListRepository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    [ExcludeFromCodeCoverage]
    public GenericListService(GenericListRepository<TItem> repository)
    {
      base._Repository = repository;
    }

    #endregion
  }
}