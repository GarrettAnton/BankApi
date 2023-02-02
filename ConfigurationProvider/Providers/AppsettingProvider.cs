using log4net;
using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationProvider.Providers
{
    public class AppsettingProvider : BaseProvider
    {
        internal IConfigurationRoot _root;

        public AppsettingProvider (ILog log) : base (log)
        {
            GetConfiguration();
        }

        public override string Url { get { return _root["Url"]; } }

        public override string ApiKey { get{
                _log.Info($"Try to get 'ApiKey' property");
                return _root["ApiKey"];
            }
        }

        public override string ExcelFileName { get{
                _log.Info($"Try to get 'ExcelFileName' property");
                return _root["ExcelFileName"];
            }
        }

        public override bool IsUSD { get {
                _log.Info($"Try to get 'IsUSD' property");
                return ParceBooleanFromSetting(_root["IsUSD"]);
            }
        }

        public override bool IsEUR { get{
                _log.Info($"Try to get 'IsEUR' property");
                return ParceBooleanFromSetting(_root["IsEUR"]);
            }
        }

        public override string UrlResource { get {
                _log.Info($"Try to get 'UrlResource' property");
                return _root["UrlResource"];
            }
        }

        internal void GetConfiguration(string filePath)
        {
            _log.Info($"Try to get use appsetting.json with path '{filePath}'");
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(filePath, false, true);
            _root = builder.Build();
        }

        internal void GetConfiguration()
        {
            GetConfiguration(Environment.CurrentDirectory + "\\appsettings.json");
        }

        internal bool ParceBooleanFromSetting (string value)
        {
            _log.Info($"Try to parse string {value} to bool");
            return bool.TryParse(value, out bool boolValue) ? boolValue : false;
        }
    }
}
