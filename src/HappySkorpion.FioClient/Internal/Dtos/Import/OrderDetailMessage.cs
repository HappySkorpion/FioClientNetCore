using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    public class OrderDetailMessage
    {
        [XmlAttribute("status")]
        public string Status { get; set; }
 
        [XmlAttribute("errorCode")]
        public int ErrorCode { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
