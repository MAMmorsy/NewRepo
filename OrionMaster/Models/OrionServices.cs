using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrionMaster.Models
{
    [XmlRoot(ElementName = "Services")]
    public class OrionServices
    {
        public OrionServices()
        {
            Servicedata = new List<ServiceData>();
            Servicetypes = new List<ServiceTypes>();
        }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "Desc")]
        public string Desc { get; set; }
        [XmlElement(ElementName = "ServiceTypes")]
        public List<ServiceTypes> Servicetypes { get; set; }

        [XmlElement(ElementName = "ServiceData")]
        public List<ServiceData> Servicedata { get; set; }

        //public FAQdata this[string name]
        //{
        //    get { return Employees.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)); }
        //}
    }

    public class ServiceTypes
    {
        public string sId { get; set; }
        public string dId { get; set; }
        public string DisplayName { get; set; }
        public string FilterName { get; set; }
        public string Img { get; set; }
    }
    public class ServiceData
    {
        public string seId { get; set; }
        public string deId { get; set; }
        public string Category { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SerImg { get; set; }
    }
}
