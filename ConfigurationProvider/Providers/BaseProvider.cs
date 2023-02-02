using BankApiInterfaces.Interfaces.Configuration;
using log4net;

namespace ConfigurationProvider.Providers
{
    public abstract class BaseProvider : IConfigurationProvider
    {
        internal ILog _log;

        public BaseProvider (ILog log)
        {
            _log = log;
        }

        public abstract string Url { get; }
        public abstract string ApiKey { get; }
        public abstract string ExcelFileName { get; }
        public abstract bool IsUSD { get; }
        public abstract bool IsEUR { get; }
        public abstract string UrlResource { get; }
    }
}
