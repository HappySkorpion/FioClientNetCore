using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    public class Sum
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("sumCredit")]
        public decimal? SumCredit { get; set; }

        [XmlElement("sumDebet")]
        public decimal? SumDebet { get; set; }
    }
}
