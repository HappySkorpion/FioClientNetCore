using System.ComponentModel;
using System.Xml.Serialization;

namespace HappySkorpion.FioClient.Internal.Dtos
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public class Info
    {
        [XmlElement("accountId")]
        public string AccountId { get; set; }

        [XmlElement("bankId")]
        public string BankId { get; set; }

        [XmlElement("currency")]
        public string Currency { get; set; }

        [XmlElement("iban")]
        public string Iban { get; set; }

        [XmlElement("bic")]
        public string Bic { get; set; }

        [XmlElement("openingBalance")]
        public decimal OpeningBalance { get; set; }

        [XmlElement("closingBalance")]
        public decimal ClosingBalance { get; set; }

        [XmlElement("dateStart")]
        public string DateStart { get; set; }

        [XmlElement("dateEnd")]
        public string DateEnd { get; set; }

        [XmlElement("yearList")]
        public int? YearList { get; set; }

        [XmlElement("idList")]
        public long? IdList { get; set; }

        [XmlElement("idFrom")]
        public long? IdFrom { get; set; }

        [XmlElement("idTo")]
        public long? IdTo { get; set; }

        [XmlElement("idLastDownload")]
        public long? IdLastDownload { get; set; }
    }
}