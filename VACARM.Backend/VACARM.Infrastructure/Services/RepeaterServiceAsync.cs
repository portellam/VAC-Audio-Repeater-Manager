using VACARM.Application.Commands;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="RepeaterRepository"/>.
  /// </summary>
  public partial class RepeaterService<TRepository, TRepeaterModel> :
    BaseService<RepeaterRepository<TRepeaterModel>, TRepeaterModel>,
    IRepeaterService<RepeaterRepository<TRepeaterModel>, TRepeaterModel>
    where TRepository :
    RepeaterRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Logic

    /// <summary>
    /// Restart a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="model">The item</param>
    private async Task<int?> RestartAsync(TRepeaterModel model)
    {
      return await ExecutableCommands.RestartAsync
        (
          model.ProcessId,
          this.ExecutableFullPathName,
          model.StartArguments,
          model.StopArguments
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Start a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="model">The item</param>
    private async Task<int?> StartAsync(TRepeaterModel model)
    {
      return await ExecutableCommands.StartAsync
        (
          model.ProcessId,
          this.ExecutableFullPathName,
          model.StartArguments
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Stop a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="model">The item</param>
    private async Task<int> StopAsync(TRepeaterModel model)
    {
      return await ExecutableCommands.StopAsync
        (
          model.ProcessId,
          model.StopArguments
        ).ConfigureAwait(false);
    }

    public async Task<bool> UpdateServiceAsync()
    {
      if (this.DeviceService == null)
      {
        this.DeviceService =
          new DeviceService<DeviceRepository<DeviceModel>, DeviceModel>();

        return false;
      }

      return await this.DeviceService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    public async Task<int?> RestartAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);
      var item = this.Repository.Get(func);

      return await this.RestartAsync(item)
        .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<int?> RestartAllAsync()
    {
      var enumerable = this.ItemRepository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this.RestartAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<int?> RestartRangeAsync
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

      var enumerable = this.ItemRepository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        yield return await this.RestartAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<int?> RestartRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      var enumerable = this.ItemRepository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        yield return await this.RestartAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async Task<int?> StartAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);
      var item = this.Repository.Get(func);

      return await this.StartAsync(item)
        .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<int?> StartAllAsync()
    {
      var enumerable = this.ItemRepository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this.StartAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<int?> StartRangeAsync
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

      var enumerable = this.ItemRepository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        yield return await this.StartAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<int?> StartRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      var enumerable = this.ItemRepository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        yield return await this.StartAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async Task<int> StopAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);
      var item = this.Repository.Get(func);

      return await this.StopAsync(item)
        .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<int> StopAllAsync()
    {
      var enumerable = this.ItemRepository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this.StopAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<int> StopRangeAsync
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

      var enumerable = this.ItemRepository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        yield return await this.StopAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<int> StopRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      var enumerable = this.ItemRepository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        yield return await this.StopAsync(item)
          .ConfigureAwait(false);
      }
    }

    #endregion
  }
}