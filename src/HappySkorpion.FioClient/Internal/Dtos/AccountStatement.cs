using System.ComponentModel;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    [XmlRoot("AccountStatement")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class AccountStatement
    {
        [XmlElement("Info")]
        public Info Info { get; set; }

        [XmlArray("TransactionList")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays")]
        public Transaction[] Transactions { get; set; }
    }
}