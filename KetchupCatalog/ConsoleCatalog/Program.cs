using System;
using Gorny.KetchupCatalog.BLC;
using Gorny.KetchupCatalog.ConsoleCatalog.Properties;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.ConsoleCatalog
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings appSettings = new Settings();
            DataProvider dataProvider = new DataProvider(appSettings.DAO);

            Console.WriteLine("Producenci");
            foreach (IProducer producer in dataProvider.Producers)
            {
                Console.WriteLine(producer.Name);
            }

            Console.WriteLine("Ketchupy");
            foreach (IKetchup ketchup in dataProvider.Ketchups)
            {
                Console.WriteLine($"{ketchup.Name} {ketchup.Type}");
            }

            Console.ReadKey();
        }
    }
}
