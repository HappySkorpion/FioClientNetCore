namespace HappySkorpion.FioClient
{
    public class DomesticTransactionOrder
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
        /// Required field.
        /// </summary>
        public CurrencyCode Currency { get; set; }

        /// <summary>
        /// Destination account bank code.
        /// One of bank codes used in CZ or SK.
        /// Format "^\d{4}$".
        /// Required field.
        /// </summary>
        public string DestinationAccountBank { get; set; }

        /// <summary>
        /// Destination account number. Account number in <see cref="DestinationAccountBank"/>.
        /// Format "^\d{1,6}-\d{1,10}$".
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
        /// Date  when the transaction has to occur. 
        /// Format "yyyy-MM-dd".
        /// Required field.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Message for the recipient.
        /// Max length 140.
        /// </summary>
        public string MessageForRecipient { get; set; }

        /// <summary>
        /// Some user's comment describing the transaction.
        /// Max length 255.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Reason for the payment.
        /// </summary>
        public PaymentReason? PaymentReason { get; set; }

        /// <summary>
        /// Payment type.
        /// </summary>
        public PaymentType? PaymentType { get; set; }
    }
}
