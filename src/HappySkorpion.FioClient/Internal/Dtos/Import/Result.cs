using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    public class Result
    {
        [XmlElement("errorCode")]
        public int ErrorCode { get; set; }

        [XmlElement("idInstruction")]
        public long? IdInstruction { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("message")]
        public string Message { get; set; }

        [XmlArray("sums")]
        [XmlArrayItem("sum")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays")]
        public Sum[] Sums { get; set; }
    }
}
