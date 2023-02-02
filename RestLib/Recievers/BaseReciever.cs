using BankApiInterfaces.Interfaces.Configuration;
using BankApiInterfaces.Interfaces.RestApi;
using log4net;
using RestSharp;

namespace RestLib.Recievers
{
    public abstract class BaseReciever : IExchangeRateReciever
    {
        internal RestClient client;
        internal ILog _log;
        internal string _resource;
        public BaseReciever(ILog iLog, IConfigurationProvider configurationProvider)
        {
            _log = iLog;
            _log.Info("Create new RestSharp client");
            client = new RestClient(configurationProvider.Url);
            _resource = configurationProvider.UrlResource;
            
        }
        public abstract string USD { get; }
        public abstract string EUR { get; }

        public virtual void Dispose()
        {
            _log.Info("Try to dispose RestSharp client");
            client.Dispose();
            _log.Info("RestSharp client was disposed");
        }
    }
}
