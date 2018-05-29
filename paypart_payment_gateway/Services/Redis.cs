using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using paypart_payment_gateway.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;

namespace paypart_payment_gateway.Services
{
    public class Redis
    {
        IOptions<Settings> settings;
        IDistributedCache redis;
        public delegate void SetBankDetail(string key, BankViewModels bankViewModel);
        public delegate void SetBankDetails(string key, IEnumerable<BankViewModels> bankViewModels);

        //public delegate void SetState(string key, State state);
        //public delegate void SetStates(string key, IEnumerable<State> states);

        public Redis(IOptions<Settings> _settings, IDistributedCache _redis)
        {
            settings = _settings;
            redis = _redis;
        }
        public async Task<AccountData> getBankDetail(string key, CancellationToken ctx)
        {
            AccountData banks = new AccountData();
            try
            {
                var bank = await redis.GetStringAsync(key, ctx);
                banks = JsonHelper.fromJson<AccountData>(bank);
            }
            catch (Exception)
            {

            }
            return banks;
        }

        public async Task<List<AccountData>> getBankDetails(string key, CancellationToken ctx)
        {
            List<AccountData> banks = new List<AccountData>();
            try
            {
                var bank = await redis.GetStringAsync(key, ctx);
                banks = JsonHelper.fromJson<List<AccountData>>(bank);
            }
            catch (Exception)
            {

            }
            return banks;
        }

        public async void setbankdetail(string key, AccountData banks)
        {
            try
            {
                var bank = await redis.GetStringAsync(key);
                if (!string.IsNullOrEmpty(bank))
                {
                    redis.Remove(key);
                }
                string value = JsonHelper.toJson(banks);

                await redis.SetStringAsync(key,value);
            }
            catch (Exception)
            {

            }

        }
        public async Task setbankdetailAsync(string key, AccountData banks, CancellationToken cts)
        {
            try
            {
                var bank = await redis.GetStringAsync(key);
                if (!string.IsNullOrEmpty(bank))
                {
                    redis.Remove(key);
                }
                string value = JsonHelper.toJson(banks);

                await redis.SetStringAsync(key, value, cts);
            }
            catch (Exception ex)
            {

            }

        }
        public async Task setbankdetails(string key, List<AccountData> banks, CancellationToken cts)
        {
            try
            {
                var bank = await redis.GetStringAsync(key);
                if (!string.IsNullOrEmpty(bank))
                {
                    redis.Remove(key);
                }
                string value = JsonHelper.toJson(banks);

                await redis.SetStringAsync(key, value, cts);
            }
            catch (Exception)
            {

            }

        }


        public async Task<AccountNameData> getaccountname(string key, CancellationToken ctx)
        {
            AccountNameData names = new AccountNameData();
            try
            {
                var name = await redis.GetStringAsync(key, ctx);
                names = JsonHelper.fromJson<AccountNameData>(name);
            }
            catch (Exception)
            {

            }
            return names;
        }

        public async Task<List<AccountNameData>> getaccountnames(string key, CancellationToken ctx)
        {
            List<AccountNameData> names = new List<AccountNameData>();
            try
            {
                var name = await redis.GetStringAsync(key, ctx);
                names = JsonHelper.fromJson<List<AccountNameData>>(name);
            }
            catch (Exception)
            {

            }
            return names;
        }

        public async void setname(string key, AccountNameData names)
        {
            try
            {
                var name = await redis.GetStringAsync(key);
                if (!string.IsNullOrEmpty(name))
                {
                    redis.Remove(key);
                }
                string value = JsonHelper.toJson(names);

                await redis.SetStringAsync(key, value);
            }
            catch (Exception)
            {

            }

        }
        public async Task setNameAsync(string key, AccountNameData names, CancellationToken cts)
        {
            try
            {
                var name = await redis.GetStringAsync(key);
                if (!string.IsNullOrEmpty(name))
                {
                    redis.Remove(key);
                }
                string value = JsonHelper.toJson(names);

                await redis.SetStringAsync(key, value, cts);
            }
            catch (Exception ex)
            {

            }

        }
        public async Task setnames(string key, List<AccountNameData> names, CancellationToken cts)
        {
            try
            {
                var name = await redis.GetStringAsync(key);
                if (!string.IsNullOrEmpty(name))
                {
                    redis.Remove(key);
                }
                string value = JsonHelper.toJson(names);

                await redis.SetStringAsync(key, value, cts);
            }
            catch (Exception)
            {

            }

        }
    }
}
