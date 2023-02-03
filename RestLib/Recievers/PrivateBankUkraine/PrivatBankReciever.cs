using BankApiInterfaces.Interfaces.Configuration;
using log4net;
using Newtonsoft.Json;
using RestLib.Recievers.PrivateBankUkraine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestLib.Recievers.PrivateBankUkraine
{
    public class PrivatBankReciever : BaseJsonReciever
    {
        internal List<PrivateBankCurrency> currencies;

        public PrivatBankReciever(ILog iLog, IConfigurationProvider configurationProvider) : base(iLog, configurationProvider)
        {
        }
        public override string USD
        {
            get
            {
                _log.Info($"Try to get USD exchange rate");
                return GetExchangeRate("USD");
            }
        }
        public override string EUR
        {
            get
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
                    currencies = JsonConvert.DeserializeObject<List<PrivateBankCurrency>>(json);
                }
                if (!currencies.Any(_ => _.Ccy == currencyCode))
                {
                    _log.Error($"The currency with code '{currencyCode}' was not found");
                    return null;
                }
                return currencies.First(_ => _.Ccy == currencyCode).Sale.ToString();
            }
            catch (Exception e)
            {
                _log.Error($"The error was ocured with the message '{e.Message}'");
                throw;
            }
        }
    }
}
