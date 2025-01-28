using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace VACARM.Core.Entities
{
  public partial class Entity :
    IEditableObject,
    INotifyPropertyChanged
  {
    #region Parameters

    private Hashtable propertyHashtable = null;

    #endregion

    #region Logic

    /// <summary>
    /// Edit the entity.
    /// </summary>
    public void BeginEdit()
    {
      //exit if in Edit mode
      if (null != propertyHashtable)
      {
        return;
      }

      //enumerate properties
      PropertyInfo[] properties = this
        .GetType()
        .GetProperties
        (
          BindingFlags.Public 
          | BindingFlags.Instance
        );

      propertyHashtable = new Hashtable(properties.Length - 1);

      for (int i = 0; i < properties.Length; i++)
      {
        //check if there is set accessor
        if (null != properties[i].GetSetMethod())
        {
          object value = properties[i]
            .GetValue(
              this, 
              null
            );

          propertyHashtable.Add
            (
              properties[i].Name, 
              value
            );
        }
      }
    }

    /// <summary>
    /// Reject edit of entity.
    /// </summary>
    public void CancelEdit()
    {
      //check for unappropriated call sequence
      if (null == propertyHashtable)
      {
        return;
      }

      //restore old values
      PropertyInfo[] properties = this
        .GetType()
        .GetProperties
        (
          BindingFlags.Public 
          | BindingFlags.Instance
        );

      for (int i = 0; i < properties.Length; i++)
      {
        //check if there is set accessor
        if (null != properties[i].GetSetMethod())
        {
          object value = propertyHashtable[properties[i].Name];

          properties[i]
            .SetValue
            (
              this, 
              value, 
              null
            );
        }
      }

      //delete current values            
      //props = null;
    }

    /// <summary>
    /// Commit edit of entity.
    /// </summary>
    public void EndEdit()
    {
      //delete current values            
      propertyHashtable = null;
      BeginEdit();
    }

    #endregion
  }
}