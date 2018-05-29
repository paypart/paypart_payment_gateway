using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paypart_payment_gateway.Services
{
    public class Settings
    {
        public string mongoUrl;
        public string connectionString;
        public string database;
        public string brokerList;
        public string addBillerTopic;
        public int redisCancellationToken;
        public string authToken;
        public string payStackBaseURL;
    }
}
