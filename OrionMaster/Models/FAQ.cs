using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrionMaster.Models
{
    //public class FAQ
    //{
    //    public string ENTitle { get; set; }
    //    public string ARTitle { get; set; }
    //    public string ENQ { get; set; }
    //    public string ARQ { get; set; }
    //    public string ENA { get; set; }
    //    public string ARA { get; set; }
    //}

    [XmlRoot(ElementName = "FAQ")]
    public class FAQ
    {
        public FAQ()
        {
            FAQdatas = new List<FAQdata>();
        }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "FAQdata")]
        public List<FAQdata> FAQdatas { get; set; }

        //public FAQdata this[string name]
        //{
        //    get { return Employees.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)); }
        //}
    }

    public class FAQdata
    {
        public string sId { get; set; }
        public string dId { get; set; }
        public string Q { get; set; }
        public string A { get; set; }
        ////[XmlAttribute("ENTitle")]
        //public string ENTitle { get; set; }
        ////[XmlAttribute("ARTitle")]
        //public string ARTitle { get; set; }
        ////[XmlAttribute("ENQ")]
        //public string ENQ { get; set; }
        ////[XmlAttribute("ARQ")]
        //public string ARQ { get; set; }
        ////[XmlAttribute("ENA")]
        //public string ENA { get; set; }
        ////[XmlAttribute("ARA")]
        //public string ARA { get; set; }
    }
}
