using System;
using System.Collections.Generic;
using System.Text;

namespace HappySkorpion.FioClient
{
    public class OrdersResult
    {
        public long? IdInstruction { get; set; }

        public string Status { get; set; }

        public int ErrorCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
        public IList<OrderResult> OrderResults { get; set; }

    }
}
