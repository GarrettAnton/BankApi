using BankApiInterfaces.Interfaces.Configuration;
using BankApiInterfaces.Interfaces.Excel;
using BankApiInterfaces.Interfaces.RestApi;

namespace BankApiInterfaces.Interfaces
{
    public interface IBankInterfacesBuilder
    {
        IExcelProcessor GetExcelProcessor();
        IExchangeRateReciever GetExchangeRateReciever(IConfigurationProvider configurationProvider);
        IConfigurationProvider GetConfigurationProvider();
    }
}
