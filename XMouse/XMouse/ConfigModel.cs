using System.Collections.Generic;
using System.Xml.Serialization;

namespace XMouse
{
    [XmlRoot(ElementName = "Processes")]
    public class Processes
    {
        [XmlElement(ElementName = "Proc")]
        public List<string> Proc { get; set; }
    }

    [XmlRoot(ElementName = "XMouseConfig")]
    public class XMouseConfig
    {
        [XmlElement(ElementName = "Processes")]
        public Processes Processes { get; set; }
    }
}
