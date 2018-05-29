using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using paypart_payment_gateway.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace paypart_payment_gateway.Services
{
    public class Utility
    {
        IOptions<Settings> settings;
        public Utility(IOptions<Settings> _settings)
        {
            settings = _settings;
        }
        public async Task<List<AccountData>> getBanks()
        {
            string resultContent = string.Empty;
            string _ContentType = "application/json";
            string authToken = settings.Value.authToken;
            BankViewModels bankdetail = new BankViewModels();
            List<AccountData> banks = new List<AccountData>();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    var result = await client.GetAsync(settings.Value.payStackBaseURL + "bank");
                    resultContent = await result.Content.ReadAsStringAsync();

                }
                bankdetail = JsonHelper.fromJson<BankViewModels>(resultContent);
                banks = bankdetail.data;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return banks;
        }
        public async Task<AccountNameData> getAccountName(string id, string acctnum)
        {
            string resultContent = string.Empty;
            string _ContentType = "application/json";
            string authToken = settings.Value.authToken;
            AccountNameData accountdet = new AccountNameData();
            AccountName acctname = new AccountName();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    var result = await client.GetAsync(settings.Value.payStackBaseURL + "bank/resolve?account_number=" + acctnum + "&bank_code=" + id);
                    resultContent = await result.Content.ReadAsStringAsync();

                }
                acctname = JsonHelper.fromJson<AccountName>(resultContent);
                accountdet = acctname.data;
            }
            catch (Exception ex)
            {
                //u.LogError(ex, "DAC -- getSBUs: ");
            }
            return accountdet;
        }
    }
}
