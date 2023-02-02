using BankApiInterfaces.Interfaces;
using BankApiInterfaces.Interfaces.Configuration;
using BankApiInterfaces.Interfaces.Excel;
using BankApiInterfaces.Interfaces.RestApi;
using log4net;

namespace BankInterfacesBuilder.Builders
{
    public abstract class BaseInterfacesBuilder : IBankInterfacesBuilder
    {
        internal ILog _iLog;
        public BaseInterfacesBuilder()
        {
            _iLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public abstract IConfigurationProvider GetConfigurationProvider();
        public abstract IExcelProcessor GetExcelProcessor();
        public abstract IExchangeRateReciever GetExchangeRateReciever(IConfigurationProvider on);
    }
}
