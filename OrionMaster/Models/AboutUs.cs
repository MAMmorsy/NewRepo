using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrionMaster.Models
{
    [XmlRoot(ElementName = "AboutUs")]
    public class AboutUs
    {
        public AboutUs()
        {
            AboutUsdatas = new List<AboutUsdata>();
        }
        [XmlElement(ElementName = "HeadInfo")]
        public string HeadInfo { get; set; }
        //[XmlElement(ElementName = "HeadInfo1")]
        //public string HeadInfo1 { get; set; }
        //[XmlElement(ElementName = "HeadInfo2")]
        //public string HeadInfo2 { get; set; }
        //[XmlElement(ElementName = "HeadInfo3")]
        //public string HeadInfo3 { get; set; }
        [XmlElement(ElementName = "AboutUsData")]
        public List<AboutUsdata> AboutUsdatas { get; set; }

    }
    public class AboutUsdata
    {
        public AboutUsdata()
        {
            Aboutdatas = new List<Aboutdata>();
        }
        [XmlElement(ElementName = "Title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "active")]
        public string Active { get; set; }
        [XmlElement(ElementName = "Subject")]
        public string Subject { get; set; }
        [XmlElement(ElementName = "Desc")]
        public string Desc { get; set; }
        [XmlElement(ElementName = "AboutData")]
        public List<Aboutdata> Aboutdatas { get; set; }
    }
    public class Aboutdata
    {
        public string sId { get; set; }
        public string dId { get; set; }
        public string Title { get; set; }
        [XmlElement(ElementName = "active")]
        public string Active { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}