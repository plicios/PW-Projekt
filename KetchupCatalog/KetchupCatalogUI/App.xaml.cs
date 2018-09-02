using System.Windows;
using Gorny.KetchupCatalog.BLC;
using Gorny.KetchupCatalog.KetchupCatalogUI.Properties;
using Gorny.KetchupCatalog.KetchupCatalogUI.ViewModels;

namespace Gorny.KetchupCatalog.KetchupCatalogUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DataProvider DataProvider { get; }
        public KetchupViewModel SelectedKetchupViewModel { get; set; }

        public App()
        {
            DataProvider = new DataProvider(new Settings().Dao);
        }
    }
}
