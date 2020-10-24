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

        [XmlElement("currency")]
        public CurrencyCode Currency { get; set; }

        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [XmlElement("accountTo")]
        public string AccountTo { get; set; }

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

        [XmlElement("bic")]
        public string Bic { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeBic()
        {
            return !string.IsNullOrEmpty(Bic);
        }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("comment")]
        public string Comment { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeComment()
        {
            return !string.IsNullOrEmpty(Comment);
        }

        [XmlElement("benefName")]
        public string BenefName { get; set; }

        [XmlElement("benefStreet")]
        public string BenefStreet { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeBenefStreet()
        {
            return !string.IsNullOrEmpty(BenefStreet);
        }

        [XmlElement("benefCity")]
        public string BenefCity { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeBenefCity()
        {
            return !string.IsNullOrEmpty(BenefCity);
        }

        [XmlElement("benefCountry")]
        public string BenefCountry { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeBenefCountry()
        {
            return !string.IsNullOrEmpty(BenefCountry);
        }

        [XmlElement("remittanceInfo1")]
        public string RemittanceInfo1 { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeRemittanceInfo1()
        {
            return !string.IsNullOrEmpty(RemittanceInfo1);
        }

        [XmlElement("remittanceInfo2")]
        public string RemittanceInfo2 { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeRemittanceInfo2()
        {
            return !string.IsNullOrEmpty(RemittanceInfo2);
        }

        [XmlElement("remittanceInfo3")]
        public string RemittanceInfo3 { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeRemittanceInfo3()
        {
            return !string.IsNullOrEmpty(RemittanceInfo3);
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
        public EuroPaymentType? PaymentType { get; set; }

        [XmlElement("paymentType")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int? PaymentTypeInt
        {
            get => (int?)PaymentType;
            set => PaymentType = (EuroPaymentType?)value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializePaymentTypeInt()
        {
            return PaymentTypeInt.HasValue;
        }
    }
}
