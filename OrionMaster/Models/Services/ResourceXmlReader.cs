using Microsoft.AspNetCore.Hosting.Internal;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace OrionMaster.Models.Services
{
    public static class ResourceXmlReader
    {
        //static property, public readable only 
        public static readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> Resources = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>(); 

       //static constructor 
       static ResourceXmlReader()
        {
            try
            {
                //string path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_GlobalResources/XmlResources/");
                string path = Path.GetFullPath("App_GlobalResources/XmlResources/");
                //FolderContentBrowser content = new FolderContentBrowser(path);
                string[] content = Directory.GetFiles(path);
                LoopOnResources(content.ToList());
            }
            catch { }
        }

        //Browse each xml resource file on the current directory 
        private static void LoopOnResources(List<string> fileList)
        {
            fileList.Where(o => o.EndsWith(".xml")).ToList().ForEach(o => OpenAndStoreResource(o));
        }

        //Open, read and store into the static property xml file 
        private static void OpenAndStoreResource(string resourcePath)
        {
            try
            {
                string fileName = Path.GetFileName(resourcePath).Split('.')[0];
                XDocument doc = XDocument.Load(resourcePath);
                if (null != doc)
                {
                    Dictionary<string,Dictionary<string,string>> currentResource = new Dictionary<string, Dictionary<string, string>> ();
                    var resources = doc.Descendants("Resource").ToList();
                    resources.ForEach(o => currentResource.Add(o.Attribute("key").Value, getEachLanguage(o.Elements("Language"))));

                    //attachement des resources à une ressource nommée 
                    Resources.Add(fileName, currentResource);
                }
            }
            catch { }
        }

        //Loop on each language into the file 
        private static Dictionary<string, string> getEachLanguage(IEnumerable<XElement> elements)
        {
            Dictionary<string, string> langList = new Dictionary<string, string>();
            elements.ToList().ForEach(o => langList.Add(o.Attribute("key").Value, o.Value));
            return langList;
        }
    }
}