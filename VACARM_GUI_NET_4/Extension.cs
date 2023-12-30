using System;
using System.IO.Packaging;
using System.Reflection;
using System.Windows.Markup;
using System.Windows.Navigation;
using Application = System.Windows.Application;

namespace VACARM_GUI_NET_4
{
    static class Extension
    {
        /// <summary>
        /// Alternate function of "Initialize Component".
        /// Original is not unit-testable, and will throw an exception of "System.Exception : The component 'Castle.Proxies.{name_of_class}Proxy' does not have a resource identified by the URI '/{name_of_namespace};component/{name_of_class}.xaml'."
        /// </summary>
        /// <param name="_object">the Object</param>
        /// <param name="baseUri">the base URI</param>
        public static void LoadViewFromUri(this Object _object, string baseUri)
        {
            try
            {
                var resourceLocater = new Uri(baseUri, UriKind.Relative);
                var exprCa = (PackagePart)typeof(Application).GetMethod("GetResourceOrContentPart", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { resourceLocater });
                var stream = exprCa.GetStream();
                var uri = new Uri((Uri)typeof(BaseUriHelper).GetProperty("PackAppBaseUri", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null), resourceLocater);

                var parserContext = new ParserContext
                {
                    BaseUri = uri
                };

                typeof(XamlReader).GetMethod("LoadBaml", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { stream, parserContext, _object, true });
            }
            catch (Exception exception)
            {
                //TODO: add logger here.
            }
        }
    }
}