using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    [XmlRoot("Import")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class Import<TTransaction>
    {
        [XmlAttribute("noNamespaceSchemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "Needed for correct XML serialization")]
        public string noNamespaceSchemaLocationAttribute = "http://www.fio.cz/schema/importIB.xsd";

        [XmlArray("Orders")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Needed for correct XML serialization")]
        public TTransaction[] Orders { get; set; }
    }
}
