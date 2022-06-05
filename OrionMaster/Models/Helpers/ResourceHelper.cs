using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OrionMaster.Models.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionMaster.Models.Helpers
{
    public static class ResourceHelper
    {
        private static IResourceService _resources;
        public static string GetResource(this HtmlHelper helper, string resourceName, string resourceKey)
        {
            CheckProvider();
            return _resources.GetResource(resourceName, resourceKey);
        }

        public static HtmlString GetJSONResources(this HtmlHelper helper, string[] resourcesName)
        {
            CheckProvider();
            string lang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
            TagBuilder builder = new TagBuilder("script");
            builder.MergeAttribute("type", "text/javascript");
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine();
            strBuilder.AppendLine("var MyApp = MyApp || {};");
            strBuilder.AppendLine("MyApp.Resources = MyApp.Resources || {};");
            strBuilder.AppendLine("MyApp.Resources =");
            strBuilder.AppendLine("{");
            resourcesName.ToList().ForEach(resourceName => {
                var ressourceCollection = _resources.GetRessourcesByName(resourceName);
                if (null != ressourceCollection && ressourceCollection.Count > 0)
                {
                    int nbElements = ressourceCollection.Count;
                    int i = 1;
                    foreach (KeyValuePair<string, Dictionary<string, string>> item in ressourceCollection)
                    {
                        string value = string.Empty;
                        try
                        {
                            value = item.Value[lang];
                        }
                        catch
                        {
                            try
                            {
                                value = item.Value["EN"];
                            }
                            catch { }
                        }
                        strBuilder.AppendFormat(@"""{0}"" : ""{1}""", item.Key, value);
                        strBuilder.Append(",");
                        strBuilder.AppendLine();
                        i++;
                    }
                }
            });
            strBuilder.Remove(strBuilder.Length - 3, 1);
            strBuilder.AppendLine("}");
            //builder.InnerHtml = strBuilder.ToString();
            builder.InnerHtml.Append(strBuilder.ToString());
            return new HtmlString(builder.ToString());
        }

        public static void RegisterProvider(IResourceService provider)
        {
            _resources = provider;
        }

        private static void CheckProvider()
        {
            if (null == _resources)
                throw new Exception("Resource provider is not set");
        }
    }
}