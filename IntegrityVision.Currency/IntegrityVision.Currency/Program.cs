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
        private int _count;
        public int Count { get; set; }

        static void Main(string[] args)
        {
            string typedCurrency;
            string[] validCurrencies = new string[] { "EUR", "USD", "CHF" };
            do
            {
                Console.WriteLine("\nSelect currency (write three symobls): EUR, USD, CHF");
                typedCurrency = Console.ReadLine();
            }
            while (!validCurrencies.Any(c => c.Equals(typedCurrency, StringComparison.InvariantCultureIgnoreCase)));

            Console.WriteLine("And now just wait 1-2 min, pls");
            var history = new CurrencyHistory(30);
            history.GetValues(typedCurrency, true);
            var methods = new Methods(history);

            string menu;
            do
            {
                Console.WriteLine("\nWrite symbol for work:");
                Console.WriteLine("1 - Minimal rate\r\n,2 - Maximum rate\r\n,3 - Average rate\r\n,4 - Days in average 5%\r\n,5 - Correlation between USD/EUR and CHF/EUR\r\n,0 - Exit\r\n");
                menu = Console.ReadLine();
                switch (menu)
                {

                    case "1":
                        Console.WriteLine("The minimim rate was: {0} \n", methods.MinRate());
                        break;
                    case "2":
                        Console.WriteLine("The maximum rate was: {0} \n", methods.MaxRate());
                        break;
                    case "3":
                        Console.WriteLine("The avarage rate is - {0} \n", methods.AverageRate());
                        break;
                    case "4":
                        Console.WriteLine("{0} days was in 5% on the average exchange rate, Avarage rate is: {1} \n",
                            methods.DaysInAvarage(), methods.AverageRate());
                        break;
                    case "5":
                        methods.Correlation();
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
