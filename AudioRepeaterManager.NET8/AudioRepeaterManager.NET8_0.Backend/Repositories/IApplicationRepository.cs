using System.ComponentModel;
using AudioRepeaterManager.NET8_0.Backend.Models;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public interface IApplicationRepository
  {
    #region Parameters

    Dictionary<uint, bool> IdToPreferLegacyApplicationDictionary { get; set; }
    Dictionary<uint, RepeaterRepository> IdToRepeaterRepositoryDictionary { get; set;}
    event PropertyChangedEventHandler PropertyChanged;
    List<ApplicationModel> List { get; set; }
    List<uint> ProcessIdList { get; }

    #endregion

    #region Logic

    void Insert
    (
      RepeaterRepository repeaterRepository,
      bool preferLegacyApplication
    );

    void Update
    (
      uint id,
      bool preferLegacyApplication
    );

    #endregion
  }
}
