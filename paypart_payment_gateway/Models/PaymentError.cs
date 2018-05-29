using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paypart_payment_gateway.Models
{
    public class PaymentError
    {
        public string error { get; set; }
        public List<string> errorDetails { get; set; }
        public string error_description { get; set; }
    }
}
