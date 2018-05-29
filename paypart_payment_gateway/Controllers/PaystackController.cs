using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using paypart_payment_gateway.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using paypart_payment_gateway.Services;
using System.Threading;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;

namespace paypart_payment_gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class paystackController : Controller
    {
        IOptions<Settings> settings;
        IDistributedCache cache;

        public paystackController(IOptions<Settings> _settings, IDistributedCache _cache)
        {
            settings = _settings;
            cache = _cache;
        }

        [HttpGet("GetBanks")]
        [ProducesResponseType(typeof(List<AccountData>), 200)]
        [ProducesResponseType(typeof(PaymentError), 400)]
        [ProducesResponseType(typeof(PaymentError), 500)]
        public async Task<IActionResult> getBanks()
        {
            Utility utility = new Utility(settings);
            PaymentError e = new PaymentError();
            List<AccountData> banks = new List<AccountData>();

            Redis redis = new Redis(settings, cache);
            string key = "all_paystack_banks";

            CancellationTokenSource cts;
            cts = new CancellationTokenSource();
            cts.CancelAfter(settings.Value.redisCancellationToken);

            // validate request
            if (!ModelState.IsValid)
            {
                var modelErrors = new List<PaymentError>();
                var eD = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        eD.Add(modelError.ErrorMessage);
                    }
                }
                e.error = ((int)HttpStatusCode.BadRequest).ToString();
                e.errorDetails = eD;

                return BadRequest(e);
            }

            try
            {
                banks = await redis.getBankDetails(key, cts.Token);

                if (banks != null && banks.Count > 0)
                {
                    return CreatedAtAction("GetBanks", banks);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            try
            {
                banks = await utility.getBanks();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            //Write to Redis
            try
            {
                if (banks != null && banks.Count > 0)
                    await redis.setbankdetails(key, banks, cts.Token);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return CreatedAtAction("GetBanks", banks);
        }
        [HttpGet("GetAccountName/{id}/{acctnum}")]
        [ProducesResponseType(typeof(AccountNameData), 200)]
        [ProducesResponseType(typeof(PaymentError), 400)]
        [ProducesResponseType(typeof(PaymentError), 500)]
        public async Task<IActionResult> getAccountName(string id, string acctnum)
        {
            Utility utility = new Utility(settings);
            PaymentError e = new PaymentError();
            AccountNameData name = new AccountNameData();

            Redis redis = new Redis(settings, cache);
            string key = id + "_" + acctnum;

            CancellationTokenSource cts;
            cts = new CancellationTokenSource();
            cts.CancelAfter(settings.Value.redisCancellationToken);

            // validate request
            if (!ModelState.IsValid)
            {
                var modelErrors = new List<PaymentError>();
                var eD = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        eD.Add(modelError.ErrorMessage);
                    }
                }
                e.error = ((int)HttpStatusCode.BadRequest).ToString();
                e.errorDetails = eD;

                return BadRequest(e);
            }

            try
            {
                name = await redis.getaccountname(key, cts.Token);

                if (name != null && !string.IsNullOrEmpty(name.account_name))
                {
                    return CreatedAtAction("GetAccountName", name);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            try
            {
                name = await utility.getAccountName(id, acctnum);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            //Write to Redis
            try
            {
                if (name != null && !string.IsNullOrEmpty(name.account_name))
                    await redis.setNameAsync(key, name, cts.Token);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return CreatedAtAction("GetAccountName", name);
        }
    }
}