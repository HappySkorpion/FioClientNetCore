namespace HappySkorpion.FioClient
{
    public class Account
    {
        public string AccountId { get; set; }

        public string BankId { get; set; }

        public string Currency { get; set; }

        public string Iban { get; set; }

        public string Bic { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal ClosingBalance { get; set; }
    }
}
