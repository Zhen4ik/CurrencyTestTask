using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IntegrityVision.Currency
{
    class Program
    {
        private static string curr;
        static void Main(string[] args)
        {
            string menu;
            do
            {
                Console.WriteLine("Select currency (write three symobls): EUR, USD, CHF");
                curr = Console.ReadLine();
            } while (curr != "EUR" && curr != "USD" && curr != "CHF" && curr != "eur" && curr != "usd" && curr != "chf");
            Console.WriteLine("And now just wait 1-2 min, pls \n");
            var history = new CurrencyHistory(30);
            history.GetValues(curr, true);
            do
            {
                Console.WriteLine("Write symbol for work:");
                Console.WriteLine("1-min, 2-max, 3-avarage_rate, 4-days_in_avarage_5%, 5-correlation between USD/EUR and CHF/EUR, 0-Exit \n");
                menu = Console.ReadLine();
                switch (menu)
                {
                   
                    case "1":
                        Console.WriteLine("The minimim rate was: {0} \n", MinRate(history).ToString());
                        break;
                    case "2":
                        Console.WriteLine("The maximum rate was: {0} \n", MaxRate(history).ToString());
                        break;
                    case "3":
                        Console.WriteLine("The avarage rate is - {0} \n", AvarageRate(history).ToString());
                        break;
                    case "4":
                        Console.WriteLine("{0} days was in 5% on the average exchange rate, Avarage rate is: {1} \n", DaysInAvarage(history).ToString(), AvarageRate(history).ToString());
                        break;
                    case "5":
                        Correlation(history);
                        break;
                    case "0":
                        Console.WriteLine("Okay, Goodbye");
                        break;
                    default:
                        Console.WriteLine("Try to choose again... \n");
                        break;
                }

            } while (menu != "0");
            Console.ReadLine();
        }
        private static CurrencyRecord MinRate(CurrencyHistory history)
        {
            CurrencyRecord minRecord = new CurrencyRecord();
            minRecord.Rate = 1000;
            foreach (CurrencyRecord currRec in history.records)
            {
                if (currRec.Rate < minRecord.Rate) minRecord = currRec;
            }
            return minRecord;
        }

        private static CurrencyRecord MaxRate(CurrencyHistory history)
        {
            CurrencyRecord maxRecord = new CurrencyRecord();
            maxRecord.Rate = 0;
            foreach (CurrencyRecord currRec in history.records)
            {
                if (currRec.Rate > maxRecord.Rate) maxRecord = currRec;
            }
            return maxRecord;
        }
        private static double AvarageRate(CurrencyHistory history)
        {
            int i = 0;
            double sum = 0;
            foreach (CurrencyRecord currRec in history.records)
            {
                sum += currRec.Rate;
                i++;
            }
            return Math.Round(sum / i, 5);
        }

        private static int DaysInAvarage(CurrencyHistory history)
        {
            int days = 0;
            double avargeRate = AvarageRate(history);
            double lowAvarageRate = avargeRate * 0.95;
            double highAvarageRate = avargeRate * 1.05;
            foreach (CurrencyRecord currRec in history.records)
            {
                if (currRec.Rate >= lowAvarageRate && currRec.Rate <= highAvarageRate)
                    days++;
            }
            return days;
        }

        private static void Correlation(CurrencyHistory history)
        {
            CurrencyHistory historyEUR = null;
            CurrencyHistory historyUSD = null;
            CurrencyHistory historyCHF = null;
            if (curr == "USD" || curr == "usd")
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyUSD = history;
                historyEUR = new CurrencyHistory(30);
                historyEUR.GetValues("EUR", false);
                historyCHF = new CurrencyHistory(30);
                historyCHF.GetValues("CHF", false);
            }

            if (curr == "EUR" || curr == "eur")
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyEUR = history;
                historyUSD = new CurrencyHistory(30);
                historyUSD.GetValues("USD", false);
                historyCHF = new CurrencyHistory(30);
                historyCHF.GetValues("CHF", false);
            }

            if (curr == "CHF" || curr == "chf")
            {
                Console.WriteLine("Wait pls 3 minutes, ty \n");
                historyCHF = history;
                historyEUR = new CurrencyHistory(30);
                historyEUR.GetValues("EUR", false);
                historyUSD = new CurrencyHistory(30);
                historyUSD.GetValues("USD", false);
            }

            for (int i = 1; i<historyEUR.records.Count-1; i++)
            {
                Console.WriteLine(historyEUR.records[i].Date);
                Console.WriteLine("USD/EUR   {0}  __________  CHF/EUR   {1} \n",                     
                    Math.Round(historyUSD.records[i].Rate / historyEUR.records[i].Rate - historyUSD.records[i - 1].Rate / historyEUR.records[i - 1].Rate, 5),
                    Math.Round(historyCHF.records[i].Rate / historyEUR.records[i].Rate - historyCHF.records[i - 1].Rate / historyEUR.records[i - 1].Rate, 5));
            }
        }
    }

}
