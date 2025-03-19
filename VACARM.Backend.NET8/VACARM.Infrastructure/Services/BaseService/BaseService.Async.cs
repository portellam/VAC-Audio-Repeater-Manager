using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class BaseService<TBaseModel>
  {
    #region Logic

    public async Task WriteAllToFile()
    {
      await BaseFileService<TBaseModel>
        .WriteJsonFileAsync
        (
          this.Repository
            .Enumerable,
          this.FilePathName
        );
    }

    public async Task ReadRangeFromFile()
    {
      var enumerable = await BaseFileService<TBaseModel>
        .ReadJsonFileAsync(this.FilePathName);

      this.Repository
        .Enumerable
        .Clear();

      this.Repository
        .AddRange(enumerable);
    }

    #endregion
  }
}