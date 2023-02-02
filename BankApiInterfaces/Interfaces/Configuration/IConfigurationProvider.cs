namespace BankApiInterfaces.Interfaces.Configuration
{
    public interface IConfigurationProvider
    {
        string Url { get; }
        string UrlResource { get; }
        string ApiKey { get; }
        bool IsUSD { get; }
        bool IsEUR { get; }
        string ExcelFileName { get; }
    }
}
