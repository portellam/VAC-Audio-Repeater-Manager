using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VACARM.Core.Entities
{
  public partial class Entity :
    INotifyDataErrorInfo,
    IEntity
  {
    #region Parameters

    private ConcurrentDictionary<string, List<string>> errorDictionary =
      new ConcurrentDictionary<string, List<string>>();

    private object @lock = new object();

    /// <summary>
    /// True/false does the entity have one or more error(s).
    /// </summary>
    public bool HasErrors
    {
      get
      {
        return errorDictionary
          .Any
          (
            keyValuePair =>
            keyValuePair.Value != null && keyValuePair.Value.Count > 0
          );
      }
    }

    /// <summary>
    /// Error changed event.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Handle error changed event.
    /// </summary>
    /// <param name="propertyName">the property name</param>
    public void OnErrorsChanged(string propertyName)
    {
      if (string.IsNullOrEmpty(propertyName))
      {
        return;
      }

      if (ErrorsChanged != null)
      {
        ErrorsChanged
          (
            this,
            new DataErrorsChangedEventArgs(propertyName)
          );
      }
    }

    /// <summary>
    /// Get enumerable of error(s) on a specific property of the entity.
    /// </summary>
    /// <param name="propertyName">the property name</param>
    /// <returns></returns>
    public IEnumerable GetErrors(string propertyName)
    {
      if (string.IsNullOrEmpty(propertyName))
      {
        return null;
      }

      List<string> errorsForName;

      errorDictionary.TryGetValue
        (
          propertyName,
          out errorsForName
        );

      return errorsForName;
    }

    /// <summary>
    /// Validate entity against its validations attributes.
    /// </summary>
    public Task ValidateAsync()
    {
      return Task.Run(() => Validate());
    }

    /// <summary>
    /// Validate entity against its validations attributes.
    /// </summary>
    public void Validate()
    {
      lock (@lock)
      {
        var validationContext = new ValidationContext
          (
            this,
            null,
            null
          );

        var validationResults = new List<ValidationResult>();

        Validator.TryValidateObject
          (
            this,
            validationContext,
            validationResults,
            true
          );

        foreach (var keyValuePair in errorDictionary.ToList())
        {
          if
          (
            validationResults
              .All
              (
                r =>
                r.MemberNames.All(m => m != keyValuePair.Key)
              )
            )
          {
            List<string> outLi;

            errorDictionary
              .TryRemove
              (
                keyValuePair.Key,
                out outLi
              );

            OnErrorsChanged(keyValuePair.Key);
          }
        }

        var query = from validationResult in validationResults
                    from memberName in validationResult.MemberNames
                    group validationResult by memberName into grouping
                    select grouping;

        foreach (var grouping in query)
        {
          var messageList = grouping
            .Select(r => r.ErrorMessage)
            .ToList();

          if (errorDictionary.ContainsKey(grouping.Key))
          {
            List<string> outLi;

            errorDictionary
              .TryRemove
              (
                grouping.Key,
                out outLi
              );
          }

          errorDictionary.TryAdd
            (
              grouping.Key,
              messageList
            );

          OnErrorsChanged(grouping.Key);
        }
      }
    }

    #endregion
  }
}