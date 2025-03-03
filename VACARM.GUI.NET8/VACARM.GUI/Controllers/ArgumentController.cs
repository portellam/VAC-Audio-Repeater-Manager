using System.Text.RegularExpressions;

namespace VACARM.GUI.Controllers
{
  public partial class ArgumentController :
    IDisposable
  {
    #region Parameters

    private Arguments Arguments { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of argument(s).</param>
    public ArgumentController(IEnumerable<string> enumerable)
    {
      this.ParseArgumentRange(enumerable);
    }

    public void ParseArgumentRange(IEnumerable<string> enumerable)
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
        this.ParseArgument(keyValuePair);
      }
    }

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

    /// <remarks>
    /// On exception or change of behavior, see <see cref="VACARM.GUI.Arguments"/>.
    /// </remarks>
    private void ParseArgument(KeyValuePair<string, string> keyValuePair)
    {
      string key = keyValuePair.Key;

      var propertyInfo = Arguments.GetType()
        .GetProperty(key);

      if (propertyInfo == null)
      {
        return;
      }

      if (!propertyInfo.CanWrite)
      {
        return;
      }

      var value = keyValuePair.Value;
      var type = propertyInfo.GetType();

      if (type == typeof(bool))
      {
        var hasValue = string.IsNullOrEmpty(value);

        propertyInfo.SetValue
        (
          this.Arguments,
          hasValue
        );

        return;
      }

      else if (type == typeof(string))
      {
        propertyInfo.SetValue
        (
          this.Arguments,
          value
        );
      }

      else if (type == typeof(string[]))
      {
        var array = value.Split('n');

        propertyInfo.SetValue
        (
          this.Arguments,
          array
        );

        return;
      }

      else
      {
        string message = string.Format
          (
            "The type '{0}' is not explicitly expected for {1}.",
            nameof(type),
            nameof(this.Arguments)
          );

        throw new NotImplementedException(message);
      }
    }

    #endregion
  }
}