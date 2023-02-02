using BankApiInterfaces.Interfaces.Configuration;
using BankApiInterfaces.Interfaces.Excel;
using BankApiInterfaces.Interfaces.RestApi;
using ConfigurationProvider.Providers;
using ExcelLib.ExcelWriter;
using RestLib.Recievers.NationalBankOfUkraine;

namespace BankInterfacesBuilder.Builders
{
    public class InterfacesBuilder : BaseInterfacesBuilder
    {
        public override IConfigurationProvider GetConfigurationProvider()
        {
            return new AppsettingProvider(_iLog);
        }

        public override IExcelProcessor GetExcelProcessor()
        {
            return new ExcelProcessor(_iLog);
        }

        public override IExchangeRateReciever GetExchangeRateReciever(IConfigurationProvider configurationProvider)
        {
            return new NbyReciever(_iLog, configurationProvider);
        }
    }
}
