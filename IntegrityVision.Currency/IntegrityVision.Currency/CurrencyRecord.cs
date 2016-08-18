using Newtonsoft.Json;

namespace IntegrityVision.Currency
{
    public class CurrencyRecord
    {
        [JsonProperty("txt")]
        public string Title { get; set; }
        [JsonProperty("rate")]
        public double Rate { get; set; }
        [JsonProperty("cc")]
        public string Name { get; set; }
        [JsonProperty("exchangedate")]
        public string Date { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/UAH\t - {1};\t{2}", Name, Rate, Date);
        }
    }
}
