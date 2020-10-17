using System.ComponentModel;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class DomesticTransaction
    {
        [XmlElement("accountFrom")]
        public string AccountFrom { get; set; }

        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [XmlElement("currency")]
        public CurrencyCode Currency { get; set; }

        [XmlElement("bankCode")]
        public string BankCode { get; set; }

        [XmlElement("accountTo")]
        public string AccountTo { get; set; }

        [XmlElement("ks")]
        public string ConstantSymbol { get; set; }

        [XmlElement("vs")]
        public string VariableSymbol { get; set; }

        [XmlElement("ss")]
        public string SpecificSymbol { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("messageForRecipient")]
        public string MessageForRecipient { get; set; }

        [XmlElement("comment")]
        public string Comment { get; set; }

        [XmlIgnore]
        public PaymentReason? PaymentReason { get; set; }

        [XmlElement("paymentReason")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int? PaymentReasonInt 
        {
            get => (int?)PaymentReason;
            set => PaymentReason = (PaymentReason?)value;
        }

        [XmlIgnore]
        public PaymentType? PaymentType { get; set; }

        [XmlElement("paymentType")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int? PaymentTypeInt
        {
            get => (int?)PaymentType;
            set => PaymentType = (PaymentType?)value;
        }
    }
}
