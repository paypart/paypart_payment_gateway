using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace paypart_payment_gateway.Models
{
    public class BankViewModels
    {
        public string message { get; set; }
        public bool status { get; set; }
        public List<AccountData> data { get; set; }
    }
    public class AccountData
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string code { get; set; }
        public string longcode { get; set; }
        public string gateway { get; set; }
        public bool active { get; set; }
        public string is_deleted { get; set; }
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
    public class AccountName
    {
        public bool status { get; set; }
        public string message { get; set; }
        public AccountNameData data { get; set; }
    }
    public class AccountNameData
    {
        public string account_number { get; set; }
        public string account_name { get; set; }
    }
}