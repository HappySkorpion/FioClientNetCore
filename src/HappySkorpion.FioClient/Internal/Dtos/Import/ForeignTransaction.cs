using System.ComponentModel;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class ForeignTransaction
    {
        [XmlElement("accountFrom")]
        public string AccountFrom { get; set; }

        [XmlElement("currency")]
        public CurrencyCode Currency { get; set; }

        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [XmlElement("accountTo")]
        public string AccountTo { get; set; }

        [XmlElement("bic")]
        public string Bic { get; set; }

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

        [XmlElement("benefCity")]
        public string BenefCity { get; set; }

        [XmlElement("benefCountry")]
        public string BenefCountry { get; set; }

        [XmlElement("remittanceInfo1")]
        public string RemittanceInfo1 { get; set; }

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

        [XmlElement("remittanceInfo4")]
        public string RemittanceInfo4 { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool ShouldSerializeRemittanceInfo4()
        {
            return !string.IsNullOrEmpty(RemittanceInfo4);
        }

        [XmlIgnore]
        public ChargeType DetailsOfCharges { get; set; }

        [XmlElement("detailsOfCharges")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int DetailsOfChargesInt
        {
            get => (int)DetailsOfCharges;
            set => DetailsOfCharges = (ChargeType)value;
        }

        [XmlIgnore]
        public PaymentReason PaymentReason { get; set; }

        [XmlElement("paymentReason")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int PaymentReasonInt
        {
            get => (int)PaymentReason;
            set => PaymentReason = (PaymentReason)value;
        }
    }
}
