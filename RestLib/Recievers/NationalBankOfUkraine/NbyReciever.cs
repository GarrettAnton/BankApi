using BankApiInterfaces.Interfaces.Configuration;
using log4net;
using Newtonsoft.Json;
using RestLib.Recievers.NationalBankOfUkraine.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Recievers.NationalBankOfUkraine
{
    public class NbyReciever : BaseReciever
    {
        internal string json;
        internal List<Currency> currencies;
        public NbyReciever(ILog iLog, IConfigurationProvider configurationProvider) : base(iLog, configurationProvider)
        {
        }

        public override string USD { get
            {
                _log.Error($"Try to get USD exchange rate");
                return GetExchangeRate("USD");
            }
        }
        public override string EUR { get
            {
                _log.Error($"Try to get EUR exchange rate");
                return GetExchangeRate("EUR");
            }
        }

        internal async Task GetExchangeRateListAsync()
        {
            try
            {
                var request = new RestRequest(_resource, Method.Get);
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
                    currencies = JsonConvert.DeserializeObject<List<Currency>>(json);
                }
                if (!currencies.Any(_ => _.Cc == currencyCode))
                {
                    _log.Error($"The currency with code '{currencyCode}' was not found");
                    return null;
                }
                return currencies.First(_ => _.Cc == currencyCode).Rate.ToString();
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }

    }
}
