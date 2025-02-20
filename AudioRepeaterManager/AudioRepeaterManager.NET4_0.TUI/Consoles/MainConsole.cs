using AudioRepeaterManager.NET4_0.TUI.Consoles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AudioRepeaterManager.NET4_0.Backend.Consoles
{
  public class MainConsole :
    IMainConsole,
    INotifyPropertyChanged
  {
    #region Parameters

    private bool automateMinimumViableSetup { get; set; }
    private bool enableAllDevices { get; set; }
    private bool enableAllRepeaterDevices { get; set; }
    private bool startAllRepeaters { get; set; }
    private bool ignoreMissingDependencies { get; set; }
    private bool ignoreWarnings { get; set; }
    private bool parseAudioRepeaterPathName { get; set; }
    private string audioRepeaterPathName { get; set; }
    private string[] arguments { get; set; }

    public bool AutomateMinimumViableSetup
    {
      get
      {
        return automateMinimumViableSetup;
      }
      set
      {
        automateMinimumViableSetup = value;
        OnPropertyChanged(nameof(automateMinimumViableSetup));
      }
    }

    public bool EnableAllDevices
    {
      get
      {
        return enableAllDevices;
      }
      set
      {
        enableAllDevices = value;
        OnPropertyChanged(nameof(enableAllDevices));
      }
    }

    public bool EnableAllRepeaterDevices
    {
      get
      {
        return enableAllRepeaterDevices;
      }
      set
      {
        enableAllRepeaterDevices = value;
        OnPropertyChanged(nameof(enableAllRepeaterDevices));
      }
    }

    public bool ParseAudioRepeaterPathName
    {
      get
      {
        return parseAudioRepeaterPathName;
      }
      set
      {
        parseAudioRepeaterPathName = value;
        OnPropertyChanged(nameof(parseAudioRepeaterPathName));
      }
    }

    public bool StartAllRepeaters
    {
      get
      {
        return startAllRepeaters;
      }
      set
      {
        startAllRepeaters = value;
        OnPropertyChanged(nameof(startAllRepeaters));
      }
    }

    public bool IgnoreMissingDependencies
    {
      get
      {
        return ignoreMissingDependencies;
      }
      set
      {
        ignoreMissingDependencies = value;
        OnPropertyChanged(nameof(ignoreMissingDependencies));
      }
    }

    public bool IgnoreWarnings
    {
      get
      {
        return ignoreWarnings;
      }
      set
      {
        ignoreWarnings = value;
        OnPropertyChanged(nameof(ignoreWarnings));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public string AudioRepeaterPathName
    {
      get
      {
        if (string.IsNullOrWhiteSpace(audioRepeaterPathName))
        {
          return Global.ExpectedExecutableFullPathName;
        }

        return audioRepeaterPathName;
      }
      set
      {
        audioRepeaterPathName = value;
        OnPropertyChanged(nameof(audioRepeaterPathName));
      }
    }

    public string FilePath { get; set; }

    public string[] Arguments
    {
      get
      {
        return arguments;
      }
      set
      {
        arguments = value;
        OnPropertyChanged(nameof(Arguments));
      }
    }

    

    #endregion

      #region Logic

    public void Main(string[] arguments)
    {

    }

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: '{1}'" +
          propertyName
        )
      );
    }

    /// <summary>
    /// Parse arguments.
    /// </summary>
    /// <param name="arguments">The arguments</param>
    private void ParseArguments(string[] arguments)
    {
      for (int i = 0; i < arguments.Length; i++)
      {
        string argument = arguments[i];
        string option = string.Empty;
        bool parseOption = false;
        ParseThisArgument(argument);
        parseOption = ParseAudioRepeaterPathName;

        if (!parseOption)
        {
          continue;
        }

        if (i++ > arguments.Length)
        {
          Console.WriteLine("Invalid option. No option specified.");
          Exit(1);
        }

        i++;
        option = arguments[i];
        i++;

        ParseThisOption(option);
      }
    }

    /// <summary>
    /// Parse an argument.
    /// </summary>
    /// <param name="argument">The argument</param>
    private void ParseThisArgument(string argument)
    {
      switch (argument.ToLower())
      {
        case "--mvp":
          AutomateMinimumViableSetup = true;
          break;

        case "--repeater-path":
          ParseAudioRepeaterPathName = true;
          return;

        case "--enable-all-devices":
          EnableAllDevices = true;
          break;

        case "--enable-all-repeaters":
          EnableAllRepeaterDevices = true;
          break;

        case "--start-all":
          StartAllRepeaters = true;
          break;

        case "--ignore-warnings":
          IgnoreWarnings = true;
          break;

        default:
          break;
      }
    }

    /// <summary>
    /// Parse an option.
    /// </summary>
    /// <param name="option">The option</param>
    private void ParseThisOption(string option)
    {
      if (ParseAudioRepeaterPathName)
      {
        AudioRepeaterPathName = option;

        if (!Path.IsPathRooted(AudioRepeaterPathName))
        {

        }
      }
    }

    public void Show()
    {
      string output = "MENU:";

      ChildConsoles.ChildConsoleTitleAndNamespace
        .Keys
        .ToList()
        .ForEach
        (
          x =>
          {
            output +=
              string.Format
              (
                "\n{1}{2}",
                x.ToString()
                  .ToUpper()
                  .FirstOrDefault(),
                x.ToString()
                  .ToLower()
                  .Substring
                  (
                    1,
                    x.ToString()
                      .Length
                  )
              );
          }
        );

      output += "Select an option: ";

      Console.WriteLine(output);
      string input = Console.ReadLine();
      string option = "";

      ChildConsoles.ChildConsoleTitleAndNamespace
        .ToList()
        .FirstOrDefault
        (
          x =>
          {
            return x.Key.StartsWith
            (
              input.Substring
              (
                0,
                1
              )
            );

          }
        );
        
    }

    public void Exit(int code)
    {
      throw new NotImplementedException();
    }

    public void Initialize()
    {
      throw new NotImplementedException();
    }

    #endregion

  }
