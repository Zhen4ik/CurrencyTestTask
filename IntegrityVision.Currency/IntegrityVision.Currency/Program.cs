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
        static void Main(string[] args)
        {
            string curr;
            string menu;
            do
            {
                Console.WriteLine("Select currency (write three symobls): EUR, USD, CHF");
                curr = Console.ReadLine();
            } while (curr!="EUR" && curr != "USD" && curr != "CHF" && curr != "eur" && curr != "usd" && curr != "chf");
            Console.WriteLine("And now just wait 1-2 min, pls");
            var history = new CurrencyHistory(30);
            history.GetValues(curr);
            do
            {
                Console.WriteLine("Write symbol for work:");
                Console.WriteLine("1-min, 2-max, 3-avarage_rate, 4-days_in_avarage_5%, 5-correlation between USD/EUR and CHF/EUR, 0-Exit");
                menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        Console.WriteLine(MinRate(history).ToString());
                        break;
                    case "2":
                        Console.WriteLine(MaxRate(history).ToString());
                        break;
                    case "3":
                        break; 
                    case "4":
                        break;
                    case "5":
                        break;
                    case "0":
                        Console.WriteLine("Okay, Goodbye");
                        break;
                    default:
                        Console.WriteLine("Try to choose again...");
                        break;
                } 
                       
            } while (menu!="0");
            Console.ReadLine();
        }
        private static CurrencyRecord MinRate(CurrencyHistory history)
        {
            CurrencyRecord minRecord = null;
            minRecord.Rate = 1000;
            foreach (CurrencyRecord currRec in history.records)
            {
                if (currRec.Rate < minRecord.Rate) minRecord = currRec;
            }
            return minRecord;
        }

        private static CurrencyRecord MaxRate(CurrencyHistory history)
        {
            CurrencyRecord maxRecord = null;
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
            return sum / i;
        }

        private static int DaysInAvarage(CurrencyHistory history)
        {
            int days = 0;
            double avargeRate = AvarageRate(history);
            double highAvarageRate = avargeRate * 0.95;
            double lowAvarageRate = avargeRate * 1.05;
            foreach (CurrencyRecord currRec in history.records)
            {
                if (currRec.Rate>=lowAvarageRate && currRec.Rate<=highAvarageRate)
                days++;
            }
            return days;
        }

    }

}
