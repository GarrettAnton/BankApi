using System;
using System.Collections.Generic;

namespace BankApiInterfaces.Interfaces.RestApi
{
    public interface IExchangeRateReciever : IDisposable
    {
        string USD { get; }
        string EUR { get; }
    }
}
