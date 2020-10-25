namespace HappySkorpion.FioClient
{
    public class EuroTransactionOrder
    {
        /// <summary>
        /// Fio account number.
        /// Format "^\d{1,16}$".
        /// Required field.
        /// </summary>
        public string SourceAccountNumber { get; set; }

        /// <summary>
        /// Amount of currencies to transfer.
        /// Required field.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Type of currencies to transfer.
        /// Has to be EURo.
        /// Required field.
        /// </summary>
        public CurrencyCode Currency { get; set; }

        /// <summary>
        /// Destination account number.
        /// Format valid IBAN.
        /// Required field.
        /// </summary>
        public string DestinationAccountNumber { get; set; }

        /// <summary>
        /// One of broadly used constant symbols.
        /// Format "^\d{4}$".
        /// </summary>
        public string ConstantSymbol { get; set; }

        /// <summary>
        /// Variable symbol.
        /// Format "^\d{1,10}$".
        /// </summary>
        public string VariableSymbol { get; set; }

        /// <summary>
        /// Specific symbol.
        /// Format "^\d{1,10}$".
        /// </summary>
        public string SpecificSymbol { get; set; }

        /// <summary>
        /// Bank identifier code or SWIFT code.
        /// Format "^[a-z]{6}[0-9a-z]{2}([0-9a-z]{3})?$".
        /// </summary>
        public string Bic { get; set; }

        /// <summary>
        /// Date  when the transaction has to occur. 
        /// Format "yyyy-MM-dd".
        /// Required field.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Some user's comment describing the transaction.
        /// Max length 140.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Name of the owner of the destination account.
        /// Max length 35.
        /// Required field.
        /// </summary>
        public string BenefName { get; set; }

        /// <summary>
        /// Street of the owner of the destination account.
        /// Max length 35.
        /// </summary>
        public string BenefStreet { get; set; }

        /// <summary>
        /// City of the owner of the destination account.
        /// Max length 35.
        /// </summary>
        public string BenefCity { get; set; }

        /// <summary>
        /// Country of the owner of the destination account.
        /// Max length 3.
        /// </summary>
        public string BenefCountry { get; set; }

        /// <summary>
        /// Remittance info.
        /// Max length 35.
        /// </summary>
        public string RemittanceInfo1 { get; set; }

        /// <summary>
        /// Remittance info.
        /// Max length 35.
        /// </summary>
        public string RemittanceInfo2 { get; set; }

        /// <summary>
        /// Remittance info.
        /// Max length 35.
        /// </summary>
        public string RemittanceInfo3 { get; set; }

        /// <summary>
        /// Reason for the payment.
        /// </summary>
        public PaymentReason? PaymentReason { get; set; }

        /// <summary>
        /// Payment type.
        /// </summary>
        public EuroPaymentType? PaymentType { get; set; }
    }
}
