using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace IntegrityVision.Currency
{
    public class CurrencyHistory
    {
        private string endpoint = "http://bank.gov.ua/NBUStatService/v1/statdirectory/exchange";
        private int _step;
        public List<CurrencyRecord> records;
        private DateTime _startPoint = new DateTime(1999, 1, 1);

        public CurrencyHistory() : this(10) { }

        public CurrencyHistory(int step)
        {
            _step = step;
            records = new List<CurrencyRecord>();
        }

        public void GetValues(string curr)
        {
            if (records.Count > 0) return;
            var tempValue = _startPoint;

            do
            {
                var date = tempValue.Year.ToString() + tempValue.Month.ToString("D2") + tempValue.Day.ToString("D2");
                var newVal = GetValue(curr, date);
                records.Add(newVal);
                Console.WriteLine(newVal);
                tempValue = tempValue.AddDays(_step);
            } while (tempValue <= DateTime.Now);
        }

        public int GetRecordsCount()
        {
            return records.Count;
        }

        private CurrencyRecord GetValue(string valcode, string date)
        {
            return GetValue(String.Format("valcode={0}&date={1}", valcode, date));
        }

        private CurrencyRecord GetValue(string parameters)
        {
            var request = WebRequest.Create(String.Format("{0}?{1}&json", endpoint, parameters));
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            string record;
            using (StreamReader reader = new StreamReader(stream))
                record = reader.ReadToEnd();
            string result = JArray.Parse(record).First.ToString();
            return JsonConvert.DeserializeObject<CurrencyRecord>(result);
        }
    }
}
