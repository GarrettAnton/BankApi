using BankApiInterfaces.Interfaces.Configuration;
using log4net;
using Newtonsoft.Json;
using RestLib.Recievers.MinFinUkraine.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RestLib.Recievers.MinFinUkraine
{
    public class MinFinReciever : BaseReciever
    {
        internal string json;
        internal string apiKey;
        internal SummaryBankCurrencies currencies;
        public MinFinReciever(ILog iLog, IConfigurationProvider configurationProvider) : base(iLog, configurationProvider)
        {
            apiKey = configurationProvider.ApiKey;
        }
        public override string USD
        {
            get
            {
                _log.Error($"Try to get USD exchange rate");
                return GetExchangeRate("usd");
            }
        }
        public override string EUR
        {
            get
            {
                _log.Error($"Try to get EUR exchange rate");
                return GetExchangeRate("eur");
            }
        }

        internal async Task GetExchangeRateListAsync()
        {
            try
            {
                var request = new RestRequest(_resource + "/" + apiKey + "/", Method.Get);
                var response = await client.GetAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (response.Content is null)
                    {
                        _log.Error($"The responce content is empty");
                        throw new Exception($"The responce content is empty");
                    }
                    json = response.Content;
                }
                else
                {
                    _log.Error($"The responce was broken. Status code is {response.StatusCode}");
                    throw new Exception($"The responce was broken. Status code is {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }

        internal string GetExchangeRate(string currencyCode)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    GetExchangeRateListAsync().GetAwaiter().GetResult();
                }
                if (currencies is null)
                {
                    currencies = JsonConvert.DeserializeObject<SummaryBankCurrencies>(json);
                }
                var pi = currencies.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .First(_ => _.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == currencyCode  );
                var value = (SummaryBankCurrency)pi?.GetValue(currencies, null);
                if (value is null || string.IsNullOrEmpty(value.Ask))
                {
                    _log.Error($"The currency rate with code '{currencyCode}' is null or empty");
                    return null;
                }
                return value.Ask;
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }
    }
}
