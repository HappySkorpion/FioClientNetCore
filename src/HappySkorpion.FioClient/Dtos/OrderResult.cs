using System;
using System.Collections.Generic;
using System.Text;

namespace HappySkorpion.FioClient
{
    public class OrderResult
    {
        public int Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
        public IList<OrderResultMessage> Messages { get; set; }
    }
}
