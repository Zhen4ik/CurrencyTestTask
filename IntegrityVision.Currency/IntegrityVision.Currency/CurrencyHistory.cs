using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace IntegrityVision.Currency
{
    public class CurrencyHistory
    {
        private const string endpoint = "http://bank.gov.ua/NBUStatService/v1/statdirectory/exchange";
        private int _step;
        private DateTime _startDate = new DateTime(1999, 1, 1);

        public List<CurrencyRecord> Records { get; set; }

        public CurrencyHistory(int step)
        {
            _step = step;
            Records = new List<CurrencyRecord>();
        }

        public void GetValues(string curr, bool show)
        {
            if (Records.Count > 0) return;
            var tempValue = _startDate;

            do
            {
                var date = tempValue.Year.ToString() + tempValue.Month.ToString("D2") + tempValue.Day.ToString("D2");
                var newVal = GetValue(curr, date);
                Records.Add(newVal);
                if (show)
                {
                    Console.WriteLine(newVal);
                }
                tempValue = tempValue.AddDays(_step);
            } while (tempValue <= DateTime.Now);
        }

        private CurrencyRecord GetValue(string valcode, string date)
        {
            string parametersString = string.Format("valcode={0}&date={1}", valcode, date);
            var request = WebRequest.Create(string.Format("{0}?{1}&json", endpoint, parametersString));
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            string record;
            using (var reader = new StreamReader(stream))
            {
                record = reader.ReadToEnd();
            }
            string result = JArray.Parse(record).First.ToString();
            return JsonConvert.DeserializeObject<CurrencyRecord>(result);
        }
    }
}
