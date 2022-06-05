using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrionMaster.Models
{
    [XmlRoot(ElementName = "Features")]
    public class Features
    {
        public Features()
        {
            Featuresdatas = new List<Featuresdata>();
        }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "Desc")]
        public string Desc { get; set; }
        [XmlElement(ElementName = "FeaturesData")]
        public List<Featuresdata> Featuresdatas { get; set; }

        //public FAQdata this[string name]
        //{
        //    get { return Employees.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)); }
        //}
    }

    public class Featuresdata
    {
        public string sId { get; set; }
        public string dId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Img { get; set; }
    }
}
