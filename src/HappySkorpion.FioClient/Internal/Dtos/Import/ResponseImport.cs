using System.ComponentModel;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    [XmlRoot("responseImport")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class ResponseImport
    {
        [XmlElement("result")]
        public Result Result { get; set; }

        [XmlArray("ordersDetails")]
        [XmlArrayItem("detail")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Needed for correct XML serialization")]
        public OrderDetail[] Orders { get; set; }
    }
}
