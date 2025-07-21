using Microsoft.EntityFrameworkCore;
using VACARM.Common;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Contexts
{
  public class Context :
    DbContext,
    IContext
  {
    #region Parameters

    public DbSet<DeviceModel> DeviceDbSet { get; set; }
    public DbSet<RepeaterModel> RepeaterDbSet { get; set; }
    public DbSet<RepeaterDeviceLinkModel> LinkDbSet { get; set; }
    private readonly static string FileExtension = ".db";

    public string FileName { get; set; } =
      Info.ReferencedApplicationAbbreviatedName + FileExtension;

    public string Path { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public Context()
    {
      var folder = Environment.SpecialFolder
        .LocalApplicationData;

      var path = Environment.GetFolderPath(folder);

      this.Path = System.IO
        .Path
        .Join
        (
          path,
          FileName
        );
    }

    protected override void OnConfiguring
    (DbContextOptionsBuilder dbContextOptionsBuilder)
    {
      string connectionString = $"Data Source={this.Path}";
      dbContextOptionsBuilder.UseSqlite(connectionString);
    }

    #endregion
  }
}