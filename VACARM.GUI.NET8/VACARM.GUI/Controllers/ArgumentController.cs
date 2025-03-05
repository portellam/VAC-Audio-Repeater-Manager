﻿using System.Text.RegularExpressions;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  public partial class ArgumentController :
    IDisposable
  {
    #region Parameters

    private Arguments Arguments { get; set; }

    private IEnumerable<string> ArgumentsNameEnumerable
    { 
      get
      {
        return typeof(Enums.Arguments).GetEnumNames();
      }
    }

    internal INIService INIService { get; set; }

    #endregion

    #region Logic

    /*
     * TODO:
     * - modify regex to account for "-", "--", delimited strings, and ranges?
     * - get a formatted string which is the enum/struct name.
     */
    private KeyValuePair<string, string> GetArgument(string argument)
    {
      var pattern = @"(\w+)=(""[^""]*""|\S+)";

      var match = Regex.Match
        (
          argument,
          pattern
        );

      if (!match.Success)
      {
        return new KeyValuePair<string, string>();
      }

      return new KeyValuePair<string, string>
        (
          match.Groups[1]
            .Value,
          match.Groups[2]
            .Value
            .Trim('"')
        );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of argument(s).</param>
    public ArgumentController(IEnumerable<string> enumerable)
    {
      this.INIService = new INIService();
      this.ParseRange();
      this.ParseRange(enumerable);
      this.Dispose();
    }

    /// <summary>
    /// Parse the enumerable of argument(s) from the
    /// <typeparamref name="INIService"/>.
    /// </summary>
    public void ParseRange()
    {
      foreach (var item in this.ArgumentsNameEnumerable)
      {
        var value = this.INIService
          .Read(item);

        if (StringExtension.IsNullOrEmptyOrWhitespace(value))
        {
          continue;
        }

        var keyValuePair = new KeyValuePair<string, string>
          (
            item,
            value
          );

        this.Arguments
          .Set(keyValuePair);
      }
    }

    /// <summary>
    /// Parse the enumerable of argument(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of argument(s)</param>
    public void ParseRange(IEnumerable<string> enumerable)
    {
      if (enumerable == null)
      {
        return;
      }

      if (enumerable.Count() == 0)
      {
        return;
      }

      foreach (var item in enumerable)
      {
        var keyValuePair = this.GetArgument(item);

        this.Arguments
          .Set(keyValuePair);
      }
    }

    /// <summary>
    /// Write the enumerable of argument(s) to the
    /// <typeparamref name="INIService"/>.
    /// </summary>
    public void WriteRange()
    {
      foreach (var item in this.ArgumentsNameEnumerable)
      {
        var value = this.Arguments
          .GetType()
          .GetProperty(item)
          .GetValue(this.Arguments)
          .ToString();

        this.INIService.Write
          (
            item,
            value
          );
      }
    }
    #endregion
  }
}