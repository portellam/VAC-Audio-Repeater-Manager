using VACARM.Application.Commands;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class RepeaterService<TRepository, TRepeaterModel>
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
    private async Task<int?> StopAsync(TRepeaterModel model)
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
          new DeviceRepositoryService<BaseRepository<DeviceModel>, DeviceModel>();

        return false;
      }

      return await this.DeviceService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    public async Task<int?> RestartAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.DoWorkAsync
        (
          this.RestartAsync,
          func
        ).ConfigureAwait(false);
    }

    public IAsyncEnumerable<int?> RestartAllAsync()
    {
      return this.DoWorkAllAsync(this.RestartAsync);
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

      return this.DoWorkRangeAsync
        (
          this.RestartAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> RestartRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.DoWorkRangeAsync
        (
          this.RestartAsync,
          func
        );
    }

    public async Task<int?> StartAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.DoWorkAsync
        (
          this.StartAsync,
          func
        ).ConfigureAwait(false);
    }

    public IAsyncEnumerable<int?> StartAllAsync()
    {
      return this.DoWorkAllAsync(this.StartAsync);
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

      return this.DoWorkRangeAsync
        (
          this.StartAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> StartRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.DoWorkRangeAsync
        (
          this.StartAsync,
          func
        );
    }

    public async Task<int?> StopAsync(uint? id)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsId(id);

      return await this.DoWorkAsync
        (
          this.StopAsync,
          func
        ).ConfigureAwait(false);
    }

    public IAsyncEnumerable<int?> StopAllAsync()
    {
      return this.DoWorkAllAsync(this.StopAsync);
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

      return this.DoWorkRangeAsync
        (
          this.StopAsync,
          func
        );
    }

    public IAsyncEnumerable<int?> StopRangeAsync(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TRepeaterModel>.ContainsIdEnumerable(idEnumerable);

      return this.DoWorkRangeAsync
        (
          this.StopAsync,
          func
        );
    }

    #endregion
  }
}