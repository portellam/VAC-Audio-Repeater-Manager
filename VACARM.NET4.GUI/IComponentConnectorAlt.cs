using System;
using System.IO.Packaging;
using System.Reflection;
using System.Windows.Markup;
using System.Windows.Navigation;
using Application = System.Windows.Application;

namespace VACARM.NET4.GUI
{
    /// <summary>
    /// Partial reimplementation of assembly class "IComponentConnector".
    /// </summary>
    static class IComponentConnectorAlt
    {
        /// <summary>
        /// Call reimplementation of assembly method "InitializeComponent".
        /// If call fails, call the assembly method.
        /// </summary>
        public static void InitializeComponent(Object _object)
        {
            Type type = _object.GetType();
            string typeName = type.Name.ToLower();
            string xamlName = $"{typeName}.xaml".ToLower();
            string uri = $"/{typeName};component/{xamlName}";

            try
            {
               InitializeComponentAlt(uri);
            }
            catch
            {
                try
                {
                    const string InitializeComponentMethodName = "InitializeComponent";
                    type.GetMethod(InitializeComponentMethodName).Invoke(_object, null);
                }
                catch
                {
                    //TODO: add logger here.
                    throw;
                }
            }
        }

        /// <summary>
        /// Loads the XAML UI content at XAML parse time.
        /// Unit-test-friendly alternative of the assembly method "IComponentConnector.InitializeComponent".
        /// </summary>
        /// <param name="baseUri">the base URI</param>
        internal static void InitializeComponentAlt(string baseUri)    //TODO: this is not working. Fix!
        {
            try
            {
                var resourceLocater = new Uri(baseUri, UriKind.Relative);
                BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;
                const string methodName1 = "GetResourceOrContentPart";

                var packagePart = (PackagePart)typeof(Application).GetMethod(methodName1, bindingFlags)
                    .Invoke(null, new object[] { resourceLocater });
                var stream = packagePart.GetStream();

                const string methodName2 = "PackAppBaseUri";

                var uri = new Uri((Uri)typeof(BaseUriHelper).GetProperty(methodName2, bindingFlags)
                    .GetValue(null, null), resourceLocater);

                var parserContext = new ParserContext
                {
                    BaseUri = uri
                };

                const string methodName3 = "LoadBaml";

                typeof(XamlReader).GetMethod(methodName3, bindingFlags)
                    .Invoke(null, new object[] { stream, parserContext, true });
            }
            catch
            {
                //TODO: add logger here.
                throw;
            }
        }    
    }
}