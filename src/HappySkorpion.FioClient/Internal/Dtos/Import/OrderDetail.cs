using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    public class OrderDetail
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlArray("messages")]
        [XmlArrayItem("message")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays")]
        public OrderDetailMessage[] Messages { get; set; }
    }
}
