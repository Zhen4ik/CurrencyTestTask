using System;
using System.Linq;

namespace IntegrityVision.Currency
{
    public class Methods
    {
        private CurrencyHistory _history;
        public string Curr
        {
            get
            {
                return _history.Records[0].Name;
            }
        }

        public Methods(CurrencyHistory history)
        {
            _history = history;
        }

        public CurrencyRecord MinRate()
        {
            return _history.Records.OrderBy(hr => hr.Rate).First();
        }

        public CurrencyRecord MaxRate()
        {
            return _history.Records.OrderBy(hr => hr.Rate).Last();
        }

        public double AverageRate()
        {
            return _history.Records.Average(hr => hr.Rate);
        }

        public int DaysInAvarage()
        {
            int days = 0;
            double avergeRate = AverageRate();
            double lowAverageRate = avergeRate * 0.95;
            double highAverageRate = avergeRate * 1.05;
            foreach (CurrencyRecord currRec in _history.Records)
            {
                if (currRec.Rate >= lowAverageRate && currRec.Rate <= highAverageRate)
                {
                    days++;
                }
            }
            return days;
        }

        public void Correlation()
        {
            CurrencyHistory historyEUR = null;
            CurrencyHistory historyUSD = null;
            CurrencyHistory historyCHF = null;
            if (Curr.Equals("USD", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyUSD = _history;
                historyEUR = new CurrencyHistory(30);
                historyEUR.GetValues("EUR", false);
                historyCHF = new CurrencyHistory(30);
                historyCHF.GetValues("CHF", false);
            }

            if (Curr.Equals("EUR", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyEUR = _history;
                historyUSD = new CurrencyHistory(30);
                historyUSD.GetValues("USD", false);
                historyCHF = new CurrencyHistory(30);
                historyCHF.GetValues("CHF", false);
            }

            if (Curr.Equals("CHF", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyCHF = _history;
                historyEUR = new CurrencyHistory(30);
                historyEUR.GetValues("EUR", false);
                historyUSD = new CurrencyHistory(30);
                historyUSD.GetValues("USD", false);
            }

            for (int i = 1; i < historyEUR.Records.Count - 1; i++)
            {
                Console.WriteLine(historyEUR.Records[i].Date);
                Console.WriteLine("USD/EUR \t  {0}  __________  CHF/EUR\t   {1} \n",
                    Math.Round(historyUSD.Records[i].Rate / historyEUR.Records[i].Rate - historyUSD.Records[i - 1].Rate / historyEUR.Records[i - 1].Rate, 5),
                    Math.Round(historyCHF.Records[i].Rate / historyEUR.Records[i].Rate - historyCHF.Records[i - 1].Rate / historyEUR.Records[i - 1].Rate, 5));
            }
        }
    }
}
