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
                Console.WriteLine("\nSelect currency (write three symobls): EUR, USD, CHF");
                curr = Console.ReadLine();
            } while (curr != "EUR" && curr != "USD" && curr != "CHF" && curr != "eur" && curr != "usd" && curr != "chf");
            Console.WriteLine("And now just wait 1-2 min, pls");
            var history = new CurrencyHistory(30);
            history.GetValues(curr, true);
            Methods Met = new Methods(history);

            do
            {
                Console.WriteLine("\nWrite symbol for work:");
                Console.WriteLine("1-min, 2-max, 3-avarage_rate, 4-days_in_avarage_5%, 5-correlation between USD/EUR and CHF/EUR, 0-Exit \n");
                menu = Console.ReadLine();
                switch (menu)
                {
                   
                    case "1":
                        Console.WriteLine("The minimim rate was: {0} \n", Met.MinRate().ToString());
                        break;
                    case "2":
                        Console.WriteLine("The maximum rate was: {0} \n", Met.MaxRate().ToString());
                        break;
                    case "3":
                        Console.WriteLine("The avarage rate is - {0} \n", Met.AvarageRate().ToString());
                        break;
                    case "4":
                        Console.WriteLine("{0} days was in 5% on the average exchange rate, Avarage rate is: {1} \n", Met.DaysInAvarage().ToString(), Met.AvarageRate().ToString());
                        break;
                    case "5":
                        Met.Correlation();
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
    }
}
