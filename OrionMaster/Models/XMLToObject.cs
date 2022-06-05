using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrionMaster.Models
{
    public class XMLToObject
    {
        public string ReturnData(string name,string lang)
        {
            Serializer ser = new Serializer();
            string path = string.Empty;
            string xmlInputData = string.Empty;
            string xmlOutputData = string.Empty;


            //path = Directory.GetCurrentDirectory() + @"\FAQdata.xml";
            //path = Path.GetFullPath("App_GlobalResources/XmlResources/FAQdata.xml");
            path=Path.Combine(AppDomain.CurrentDomain.BaseDirectory, lang, name);
            path = Path.GetFullPath("/App_GlobalResources/XmlResources/"+lang+@"/"+name);
            xmlInputData = File.ReadAllText(path);
            return xmlInputData;
        }

        public string ReturnDataNew(string fullPath)
        {
            Serializer ser = new Serializer();
            string path = string.Empty;
            string xmlInputData = string.Empty;
            string xmlOutputData = string.Empty;
            xmlInputData = File.ReadAllText(fullPath);
            return xmlInputData;
        }
    }
}
