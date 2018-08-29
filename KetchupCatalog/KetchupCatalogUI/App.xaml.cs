using System.Windows;
using Gorny.KetchupCatalog.BLC;
using Gorny.KetchupCatalog.KetchupCatalogUI.Properties;

namespace KetchupCatalogUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DataProvider DataProvider { get; }

        public App()
        {
            DataProvider = new DataProvider(new Settings().Dao);
        }
    }
}
