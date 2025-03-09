using System.Collections.Generic;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Functions;

namespace VACARM.Infrastructure.Services
{
  public partial class RepeaterGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TRepeaterModel
    >
  {
    #region Logic

    public IAsyncEnumerable<int?> RestartAllAsync()
    {
      return this.SelectedService
        .DoActionAllAsync(this.RestartAsync);
    }

    public IAsyncEnumerable<int?> RestartRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.SelectedService
        .DoActionRangeAsync
        (
          this.RestartAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> RestartRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.SelectedService
        .DoActionRangeAsync
        (
          this.RestartAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> StartAllAsync()
    {
      return this.SelectedService
        .DoActionAllAsync(this.StartAsync);
    }

    public IAsyncEnumerable<int?> StartRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.SelectedService
        .DoActionRangeAsync
        (
          this.StartAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> StartRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.SelectedService
        .DoActionRangeAsync
        (
          this.StartAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> StopAllAsync()
    {
      return this.SelectedService
        .DoActionAllAsync(this.StopAsync);
    }

    public IAsyncEnumerable<int?> StopRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.SelectedService
        .DoActionRangeAsync
        (
          this.StopAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> StopRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.SelectedService
        .DoActionRangeAsync
        (
          this.StopAsync,
          func
        );
    }

    #endregion
  }
}