using System;
using System.Collections.Generic;
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
            if (_object is null)
            {
                throw new ArgumentNullException(nameof(_object));
            }

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
        /// Unit-test-friendly alternative of the assembly method "IComponentConnector.InitializeComponent".
        /// </summary>
        /// <param name="baseUri">the base URI</param>
        internal static void InitializeComponentAlt(string baseUri)    //TODO: this is not working. Fix!
        {
            if (baseUri is null || String.Equals(baseUri, String.Empty))
            {
                return;
            }

            try
            {
                LoadXAML(baseUri);
            }
            catch
            {
                //TODO: add logger here.
                throw;
            }
        }

        /// <summary>
        /// Loads the XAML UI content at XAML parse time.
        /// </summary>
        /// <param name="baseUri">the base URI</param>
        internal static void LoadXAML(string baseUri)
        {
            var resourceLocater = new Uri(baseUri, UriKind.Relative);
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;
            const string firstMethodName = "GetResourceOrContentPart";
            var firstMethodParameters = new object[] { resourceLocater };

            var packagePart = (PackagePart)typeof(Application)
                .GetMethod(firstMethodName, bindingFlags)
                .Invoke(null, firstMethodParameters);

            var stream = packagePart.GetStream();
            const string secondConstructorName = "PackAppBaseUri";

            var baseUriHelper = (Uri)typeof(BaseUriHelper)
                .GetProperty(secondConstructorName, bindingFlags)
                .GetValue(null, null);

            var parserContext = new ParserContext
            {
                BaseUri = new Uri(baseUriHelper, resourceLocater)
            };

            const string thirdMethodName = "LoadBaml";
            var thirdMethodParameters = new object[] { stream, parserContext, true };

            typeof(XamlReader)
                .GetMethod(thirdMethodName, bindingFlags)
                .Invoke(null, thirdMethodParameters);
        }
    }
}