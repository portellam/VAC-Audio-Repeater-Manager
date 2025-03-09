using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace VACARM.Infrastructure.Services
{
  public class INIService
  {
    #region Parameters

    private const string LibraryName = "kernel32";
    private const string Extension = ".ini";

    public string ExecutableName
    {
      get
      {
        return Assembly.GetExecutingAssembly()
          .GetName()
          .Name;
      }
    }
    public string PathName { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="IniPathName">the path name</param>
    public INIService(string IniPathName = null)
    {
      var pathName = IniPathName ?? ExecutableName + Extension;
      this.PathName = new FileInfo(pathName).FullName;
    }

    public bool KeyExists
    (
      string Key,
      string Section = null
    )
    {
      return this.Read
        (
          Key,
          Section
        )
        .Length > 0;
    }

    public string Read
    (
      string Key,
      string Section = null
    )
    {
      var result = new StringBuilder(255);

      GetPrivateProfileString
        (
          Section ?? ExecutableName,
          Key,
          string.Empty,
          result,
          255,
          PathName
        );

      return result.ToString();
    }

    public void DeleteKey
    (
      string Key,
      string Section = null
    )
    {
      this.Write
        (
          Key,
          null,
          Section ?? ExecutableName
        );
    }

    public void DeleteSection(string Section = null)
    {
      this.Write
        (
          null,
          null,
          Section ?? ExecutableName
        );
    }

    public void Write
    (
      string Key,
      string Value,
      string Section = null
    )
    {
      WritePrivateProfileString
        (
          Section ?? ExecutableName, 
          Key, 
          Value, 
          PathName
        );
    }

    #endregion

    #region Modifiers

    [DllImport
      (
        LibraryName,
        CharSet = CharSet.Unicode
      )
    ]

    static extern long WritePrivateProfileString
    (
      string Section,
      string Key,
      string Value,
      string FilePath
    );

    [DllImport
      (
        LibraryName,
        CharSet = CharSet.Unicode
      )
    ]
    static extern int GetPrivateProfileString
    (
      string Section,
      string Key,
      string Default,
      StringBuilder RetVal,
      int Size,
      string FilePath
    );

    #endregion
  }
}