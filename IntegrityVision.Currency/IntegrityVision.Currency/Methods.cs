using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrityVision.Currency
{
    public class Methods
    {
        private CurrencyHistory _history;
        public string Curr
        {
            get
            {
                return _history.records[0].Name;
            }
        }

        public Methods(CurrencyHistory histoty)
        {
            _history = histoty;
        }

        public CurrencyRecord MinRate()
        {
            CurrencyRecord minRecord = new CurrencyRecord();
            minRecord.Rate = 1000;
            foreach (CurrencyRecord currRec in _history.records)
            {
                if (currRec.Rate < minRecord.Rate) minRecord = currRec;
            }
            return minRecord;
        }

        public CurrencyRecord MaxRate()
        {
            CurrencyRecord maxRecord = new CurrencyRecord();
            maxRecord.Rate = 0;
            foreach (CurrencyRecord currRec in _history.records)
            {
                if (currRec.Rate > maxRecord.Rate) maxRecord = currRec;
            }
            return maxRecord;
        }

        public double AvarageRate()
        {
            int i = 0;
            double sum = 0;
            foreach (CurrencyRecord currRec in _history.records)
            {
                sum += currRec.Rate;
                i++;
            }
            return Math.Round(sum / i, 5);
        }

        public int DaysInAvarage()
        {
            int days = 0;
            double avargeRate = AvarageRate();
            double lowAvarageRate = avargeRate * 0.95;
            double highAvarageRate = avargeRate * 1.05;
            foreach (CurrencyRecord currRec in _history.records)
            {
                if (currRec.Rate >= lowAvarageRate && currRec.Rate <= highAvarageRate)
                    days++;
            }
            return days;
        }

        public void Correlation()
        {
            CurrencyHistory historyEUR = null;
            CurrencyHistory historyUSD = null;
            CurrencyHistory historyCHF = null;
            if (Curr == "USD" || Curr == "usd")
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyUSD = _history;
                historyEUR = new CurrencyHistory(30);
                historyEUR.GetValues("EUR", false);
                historyCHF = new CurrencyHistory(30);
                historyCHF.GetValues("CHF", false);
            }

            if (Curr == "EUR" || Curr == "eur")
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyEUR = _history;
                historyUSD = new CurrencyHistory(30);
                historyUSD.GetValues("USD", false);
                historyCHF = new CurrencyHistory(30);
                historyCHF.GetValues("CHF", false);
            }

            if (Curr == "CHF" || Curr == "chf")
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyCHF = _history;
                historyEUR = new CurrencyHistory(30);
                historyEUR.GetValues("EUR", false);
                historyUSD = new CurrencyHistory(30);
                historyUSD.GetValues("USD", false);
            }

            for (int i = 1; i < historyEUR.records.Count - 1; i++)
            {
                Console.WriteLine(historyEUR.records[i].Date);
                Console.WriteLine("USD/EUR   {0}  __________  CHF/EUR   {1} \n",
                    Math.Round(historyUSD.records[i].Rate / historyEUR.records[i].Rate - historyUSD.records[i - 1].Rate / historyEUR.records[i - 1].Rate, 5),
                    Math.Round(historyCHF.records[i].Rate / historyEUR.records[i].Rate - historyCHF.records[i - 1].Rate / historyEUR.records[i - 1].Rate, 5));
            }
        }
    }
}
