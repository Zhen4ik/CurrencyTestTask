using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            return String.Format("{0}/UAH - {1};{2}", Name, Rate, Date);
        }
    }
}
