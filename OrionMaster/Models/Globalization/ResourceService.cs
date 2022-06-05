using OrionMaster.Models.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OrionMaster.Models.Globalization
{
    public class ResourceService :IResourceService
    {
        public string GetResource(string resourceName, string resourceKey)
        {
            try
            {
                string language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
                if (ResourceXmlReader.Resources.ContainsKey(resourceName))
                {
                    if (ResourceXmlReader.Resources[resourceName].ContainsKey(resourceKey))
                    {
                        if (ResourceXmlReader.Resources[resourceName][resourceKey].ContainsKey(language))
                            return ResourceXmlReader.Resources[resourceName][resourceKey][language];
                        else
                            return ResourceXmlReader.Resources[resourceName][resourceKey]["EN"];
                    }
                    else
                        return string.Empty;
                }
                else return string.Empty;
            }
            catch { return string.Empty; }
        }

        public Dictionary<string, Dictionary<string, string>> GetRessourcesByName(string resourceName)
        {
            try
            {
                return ResourceXmlReader.Resources[resourceName];
            }
            catch { return null; }
        }
    }
}
