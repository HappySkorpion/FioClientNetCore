using System.ComponentModel;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class T2Transaction
    {
        [XmlElement("accountFrom")]
        public string AccountFrom { get; set; }

        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [XmlElement("currency")]
        public CurrencyCode Currency { get; set; }

        [XmlElement("accountTo")]
        public string AccountTo { get; set; }

        [XmlElement("ks")]
        public string ConstantSymbol { get; set; }

        [XmlElement("vs")]
        public string VariableSymbol { get; set; }

        [XmlElement("ss")]
        public string SpecificSymbol { get; set; }

        [XmlElement("bic")]
        public string Bic { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("comment")]
        public string Comment { get; set; }

        [XmlElement("benefName")]
        public string BenefName { get; set; }

        [XmlElement("benefStreet")]
        public string BenefStreet { get; set; }

        [XmlElement("benefCity")]
        public string BenefCity { get; set; }

        [XmlElement("benefCountry")]
        public string BenefCountry { get; set; }

        [XmlElement("remittanceInfo1")]
        public string RemittanceInfo1 { get; set; }

        [XmlElement("remittanceInfo2")]
        public string RemittanceInfo2 { get; set; }

        [XmlElement("remittanceInfo3")]
        public string RemittanceInfo3 { get; set; }

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
