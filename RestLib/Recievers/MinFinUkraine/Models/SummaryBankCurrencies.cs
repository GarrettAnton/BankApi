using Newtonsoft.Json;

namespace RestLib.Recievers.MinFinUkraine.Models
{
    public class SummaryBankCurrency
    {
        [JsonProperty("bid")]
        public string Bid { get; set; }

        [JsonProperty("ask")]
        public string Ask { get; set; }

        [JsonProperty("trendAsk")]
        public double TrendAsk { get; set; }

        [JsonProperty("trendBid")]
        public double TrendBid { get; set; }
    }

    public class SummaryBankCurrencies
    {
        [JsonProperty("usd")]
        public SummaryBankCurrency Usd { get; set; }

        [JsonProperty("eur")]
        public SummaryBankCurrency Eur { get; set; }

        [JsonProperty("rub")]
        public SummaryBankCurrency Rub { get; set; }

        [JsonProperty("gbp")]
        public SummaryBankCurrency Gbp { get; set; }

        [JsonProperty("pln")]
        public SummaryBankCurrency Pln { get; set; }

        [JsonProperty("chf")]
        public SummaryBankCurrency Chf { get; set; }

        [JsonProperty("czk")]
        public SummaryBankCurrency Czk { get; set; }

        [JsonProperty("cad")]
        public SummaryBankCurrency Cad { get; set; }

        [JsonProperty("huf")]
        public SummaryBankCurrency Huf { get; set; }
    }
}
