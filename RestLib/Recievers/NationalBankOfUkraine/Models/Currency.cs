using Newtonsoft.Json;

namespace RestLib.Recievers.NationalBankOfUkraine.Models
{
    public class Currency
    {
        [JsonProperty("r030")]
        public int R030 { get; set; }

        [JsonProperty("txt")]
        public string Txt { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("cc")]
        public string Cc { get; set; }

        [JsonProperty("exchangedate")]
        public string Exchangedate { get; set; }
    }
}
