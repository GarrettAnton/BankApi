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
    public class NbyReciever : BaseJsonReciever
    {
        internal List<Currency> currencies;
        public NbyReciever(ILog iLog, IConfigurationProvider configurationProvider) : base(iLog, configurationProvider)
        {
        }

        public override string USD { get
            {
                _log.Info($"Try to get USD exchange rate");
                return GetExchangeRate("USD");
            }
        }
        public override string EUR { get
            {
                _log.Info($"Try to get EUR exchange rate");
                return GetExchangeRate("EUR");
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
