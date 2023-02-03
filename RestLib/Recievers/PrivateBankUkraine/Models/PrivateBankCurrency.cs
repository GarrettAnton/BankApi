using Newtonsoft.Json;

namespace RestLib.Recievers.PrivateBankUkraine.Models
{
    public class PrivateBankCurrency
    {
        [JsonProperty("ccy")]
        public string Ccy { get; set; }

        [JsonProperty("base_ccy")]
        public string BaseCcy { get; set; }

        [JsonProperty("buy")]
        public string Buy { get; set; }

        [JsonProperty("sale")]
        public string Sale { get; set; }
    }
}
