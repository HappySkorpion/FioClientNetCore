using System.Collections.Generic;

namespace HappySkorpion.FioClient
{
    public class AccountTransactionsResult
    {
        public Account Account { get; set; }

        public AccountTransactionsMetadata Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
        public IList<AccountTransaction> Transactions { get; set; }
    }
}
