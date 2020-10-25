namespace HappySkorpion.FioClient
{
    public class AccountTransaction
    {
        public long Id { get; set; }

        public string Date { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string CounterpartAccount { get; set; }

        public string CounterpartAccountName { get; set; }

        public string CounterpartBankCode { get; set; }

        public string CounterpartBankName { get; set; }

        public string ConstantSymbol { get; set; }

        public string VariableSymbol { get; set; }

        public string SpecificSymbol { get; set; }

        public string Identification { get; set; }

        public string MessageForReceipient { get; set; }

        public string Type { get; set; }

        public string Accountant { get; set; }

        public string Comment { get; set; }

        public long InstructionId { get; set; }
    }
}
