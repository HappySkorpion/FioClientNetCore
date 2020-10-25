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

        [XmlElement("currency")]
        public CurrencyCode Currency { get; set; }

        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [XmlElement("accountTo")]
        public string AccountTo { get; set; }

        [XmlElement("bankCode")]
        public string BankCode { get; set; }

        [XmlElement("ks")]
        public string ConstantSymbol { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeConstantSymbol()
        {
            return !string.IsNullOrEmpty(ConstantSymbol);
        }

        [XmlElement("vs")]
        public string VariableSymbol { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeVariableSymbol()
        {
            return !string.IsNullOrEmpty(VariableSymbol);
        }

        [XmlElement("ss")]
        public string SpecificSymbol { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeSpecificSymbol()
        {
            return !string.IsNullOrEmpty(SpecificSymbol);
        }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("messageForRecipient")]
        public string MessageForRecipient { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeMessageForRecipient()
        {
            return !string.IsNullOrEmpty(MessageForRecipient);
        }

        [XmlElement("comment")]
        public string Comment { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeComment()
        {
            return !string.IsNullOrEmpty(Comment);
        }

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

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializePaymentReasonInt()
        {
            return PaymentReasonInt.HasValue;
        }

        [XmlIgnore]
        public DomesticPaymentType? PaymentType { get; set; }

        [XmlElement("paymentType")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int? PaymentTypeInt
        {
            get => (int?)PaymentType;
            set => PaymentType = (DomesticPaymentType?)value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializePaymentTypeInt()
        {
            return PaymentTypeInt.HasValue;
        }
    }
}
