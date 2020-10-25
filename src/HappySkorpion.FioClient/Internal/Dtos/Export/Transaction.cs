using System.ComponentModel;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class Transaction
    {
        [XmlElement("column_22")]
        public long Id { get; set; }

        [XmlElement("column_0")]
        public string Date { get; set; }

        [XmlElement("column_1")]
        public decimal Amount { get; set; }

        [XmlElement("column_14")]
        public string Currency { get; set; }

        [XmlElement("column_2")]
        public string CounterpartAccount { get; set; }

        [XmlElement("column_10")]
        public string CounterpartAccountName { get; set; }

        [XmlElement("column_3")]
        public string CounterpartBankCode { get; set; }

        [XmlElement("column_12")]
        public string CounterpartBankName { get; set; }

        [XmlElement("column_4")]
        public string ConstantSymbol { get; set; }

        [XmlElement("column_5")]
        public string VariableSymbol { get; set; }

        [XmlElement("column_6")]
        public string SpecificSymbol { get; set; }

        [XmlElement("column_7")]
        public string Identification { get; set; }

        [XmlElement("column_16")]
        public string MessageForReceipient { get; set; }

        [XmlElement("column_8")]
        public string Type { get; set; }

        [XmlElement("column_9")]
        public string Accountant { get; set; }

        [XmlElement("column_25")]
        public string Comment { get; set; }

        [XmlElement("column_17")]
        public long InstructionId { get; set; }
    }
}
