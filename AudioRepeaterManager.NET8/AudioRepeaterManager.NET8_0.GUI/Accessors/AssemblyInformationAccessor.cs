using System.Reflection;

namespace VACARM.GUI.Accessors
{
  public class AssemblyInformationAccessor
  {
    #region Parameters

    public static string AssemblyCompany
    {
      get
      {
        object[] attributes = Assembly
          .GetExecutingAssembly()
          .GetCustomAttributes(
            typeof(AssemblyCompanyAttribute),
            false
          );

        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        return ((AssemblyCompanyAttribute)attributes[0]).Company;
      }
    }

    public static string AssemblyCopyright
    {
      get
      {
        object[] attributes = Assembly
          .GetExecutingAssembly()
          .GetCustomAttributes
          (
            typeof(AssemblyCopyrightAttribute),
            false
          );

        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    public static string AssemblyDescription
    {
      get
      {
        object[] attributes = Assembly
          .GetExecutingAssembly()
          .GetCustomAttributes
          (
            typeof(AssemblyDescriptionAttribute),
            false
          );

        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    public static string AssemblyProduct
    {
      get
      {
        object[] attributes = Assembly
          .GetExecutingAssembly()
          .GetCustomAttributes
          (
            typeof(AssemblyProductAttribute),
            false
          );

        if (attributes.Length == 0)
        {
          return string.Empty;
        }

        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    public static string AssemblyVersion
    {
      get
      {
        return Assembly
          .GetExecutingAssembly()
          .GetName()
          .Version
          .ToString();
      }
    }

    #endregion
  }
}