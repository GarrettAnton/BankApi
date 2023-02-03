using BankApiInterfaces.Interfaces.Configuration;
using log4net;
using RestSharp;
using System.Threading.Tasks;
using System;

namespace RestLib.Recievers
{
    public abstract class BaseJsonReciever : BaseReciever
    {
        internal string json;

        public BaseJsonReciever(ILog iLog, IConfigurationProvider configurationProvider) : base(iLog, configurationProvider)
        {
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
    }
}
